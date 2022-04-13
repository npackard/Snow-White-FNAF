using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayDoor : MonoBehaviour
{
    public int doorIndex;
    public bool open = false;
    public AudioClip doorOpen;
    public AudioClip doorLocked;

    private Animator anim;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        open = true;
        anim.SetBool("Open", true);
        audio.PlayOneShot(doorOpen);
    }

    public void LockedDoor()
    {
        if (!audio.isPlaying) audio.PlayOneShot(doorLocked);
    }
}
