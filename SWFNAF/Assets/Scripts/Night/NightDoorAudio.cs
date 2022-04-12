using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightDoorAudio : MonoBehaviour
{    
    public AudioClip doorOpen;
    public AudioClip doorClose;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor() {
        audio.PlayOneShot(doorOpen);
    }

    public void CloseDoor() {
        audio.PlayOneShot(doorClose);
    }
}
