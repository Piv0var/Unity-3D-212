using UnityEngine;

public class Gate1Script : MonoBehaviour
{
    private string closedMessage = "Двери закрыты!\r\nнайди ключ";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            bool r = Random.value < 0.5f;
            MessagesScript.ShowMessage(closedMessage + r);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}