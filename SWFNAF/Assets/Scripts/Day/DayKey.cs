using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayKey : MonoBehaviour
{
    // 0 means open by default
    // 1 -> study, 2 -> bathroom, 3-> dwarf bedroom, 4 -> workspace, 5 -> mining
    public int keyIndex;
    public AudioClip keyClip;
    private AudioSource audio;

    private MeshRenderer mr;
    private BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();

        bool key1 = PlayerPrefs.GetInt("Key1") == 1;
        bool key2 = PlayerPrefs.GetInt("Key2") == 1;
        bool key3 = PlayerPrefs.GetInt("Key3") == 1;
        bool key4 = PlayerPrefs.GetInt("Key4") == 1;
        bool key5 = PlayerPrefs.GetInt("Key5") == 1;
        bool allKeys = key1 && key2 && key3 && key4;

        if ((keyIndex == 3 || keyIndex == 4) && key1 && key2) gameObject.SetActive(true);
        if (keyIndex == 5 && allKeys) gameObject.SetActive(true);

        if (keyIndex == 1 && key1) gameObject.SetActive(false);
        if (keyIndex == 2 && key2) gameObject.SetActive(false);
        if (keyIndex == 3 && key3) gameObject.SetActive(false);
        if (keyIndex == 4 && key4) gameObject.SetActive(false);
        if (keyIndex == 5 && key5) gameObject.SetActive(false);
    }

    public void PlayAudio()
    {
        audio.PlayOneShot(keyClip);
        mr.enabled = false;
        bc.enabled = false;
    }
}
