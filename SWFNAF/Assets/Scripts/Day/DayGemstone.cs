using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayGemstone : MonoBehaviour
{
    public int gemIndex;
    public AudioClip gemClip;
    private AudioSource audio;
    private bool played = false;

    private MeshRenderer mr;
    private BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();

        bool gem1 = PlayerPrefs.GetInt("Gem1") == 1;
        bool gem2 = PlayerPrefs.GetInt("Gem2") == 1;
        bool gem3 = PlayerPrefs.GetInt("Gem3") == 1;
        bool gem4 = PlayerPrefs.GetInt("Gem4") == 1;
        bool gem5 = PlayerPrefs.GetInt("Gem3") == 1;
        bool gem6 = PlayerPrefs.GetInt("Gem4") == 1;

        if ((gemIndex == 3 || gemIndex == 4) && gem1 && gem2) gameObject.SetActive(true);
        if ((gemIndex == 5 || gemIndex == 6) && gem3 && gem4) gameObject.SetActive(true);

        if (gemIndex == 1 && gem1) gameObject.SetActive(false);
        if (gemIndex == 2 && gem2) gameObject.SetActive(false);
        if (gemIndex == 3 && gem3) gameObject.SetActive(false);
        if (gemIndex == 4 && gem4) gameObject.SetActive(false);
        if (gemIndex == 5 && gem5) gameObject.SetActive(false);
        if (gemIndex == 6 && gem6) gameObject.SetActive(false);
    }

    public void PlayAudio()
    {
        audio.PlayOneShot(gemClip);
        mr.enabled = false;
        bc.enabled = false;
    }
}
