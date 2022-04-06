using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightAudioManager : MonoBehaviour
{
    public AudioClip nightAmbience;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = nightAmbience;
        audio.loop = true;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
