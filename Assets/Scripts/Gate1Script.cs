using UnityEngine;

public class Gate1Script : MonoBehaviour
{
    private string closedMessage = "Двери закрыты!\r\nнайди ключ";
    private AudioSource closedSound;

 

    void Start()
    {
        closedSound = GetComponent<AudioSource>();
        GameState.AddChangeListener(
          OnSoundsVolumeChanged,
          nameof(GameState.effectsVolume));
        GameState.AddChangeListener(
         OnSoundsVolumeChanged,
         nameof(GameState.isSoundsMuted));

    }

    void Update()
    {

    }   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            //bool r = Random.value < 0.5f;
            MessagesScript.ShowMessage(closedMessage);
            closedSound.Play();
        }
    }

    private void OnSoundsVolumeChanged(string name) 
    {
        closedSound.volume = GameState.effectsVolume;

    }

    private void OnDestroy()
    {
        GameState.RemoveChangeListener(
            OnSoundsVolumeChanged,
            nameof(GameState.effectsVolume));
        GameState.RemoveChangeListener(
            OnSoundsVolumeChanged,
            nameof(GameState.isSoundsMuted));
    }
}