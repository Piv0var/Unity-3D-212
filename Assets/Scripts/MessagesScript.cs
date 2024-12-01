using System.Collections.Generic;
using UnityEngine;

public class MessagesScript : MonoBehaviour
{
    private float timeout = 5.0f;
    private float leftTime;
    private GameObject content;
    private TMPro.TextMeshProUGUI messageTMP;
    private static MessagesScript instance;
    private static Queue<Message> messageQueue = new Queue<Message>();

    void Start()
    {
        instance = this;
        content = transform.Find("Content").gameObject;
        messageTMP = transform.Find("Content/MessageText")
            .GetComponent<TMPro.TextMeshProUGUI>();
        leftTime = 0;
    }

    void Update()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            if (leftTime <= 0)
            {
                messageQueue.Dequeue();
                content.SetActive(false);
            }
        }
        else
        {
            if (messageQueue.Count > 0)
            {
                Message message = messageQueue.Peek();

                messageTMP.text = string.IsNullOrEmpty(message.author)
                    ? message.text
                    : $"{message.author}: {message.text}";

                leftTime = message.timeout ?? this.timeout;
                content.SetActive(true);
            }
        }
    }


    public static void ShowMessage(string message, string author = null, float? timeout = null)
    {
        foreach (var msg in messageQueue)
        {
            if (msg.text == message && msg.author == author)
            {
                Debug.Log($"Message '{message}' from author '{author}' ignored");
                return;
            }
        }

        messageQueue.Enqueue(new Message
        {
            text = message,
            author = author,
            timeout = timeout
        });

        Debug.Log($"Message '{message}' from author '{author}' added to queue");
    }

    private class Message
    {
        public string text { get; set; }      
        public string author { get; set; }   
        public float? timeout { get; set; }  
    }
}
