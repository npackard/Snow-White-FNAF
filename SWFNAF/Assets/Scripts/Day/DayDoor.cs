using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayDoor : MonoBehaviour
{
    public int doorIndex;

    public bool open = false;
    public AudioClip doorOpen;
    public AudioClip doorLocked;

    public GameObject[] cams;

    private Animator anim;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        foreach (GameObject cam in cams)
        {
            cam.SetActive(false);
        }
    }

    public void OpenDoor()
    {
        open = true;
        if (anim) anim.SetBool("Open", true);
        audio.PlayOneShot(doorOpen);

        foreach (GameObject cam in cams)
        {
            cam.SetActive(true);
        }

        if (doorIndex == 6)
        {
            DayGameManager.instance.GameEnding(); // placeholder if we want to exit through front door
        }
    }

    public void LockedDoor()
    {
        if (!audio.isPlaying) audio.PlayOneShot(doorLocked);
    }
}
