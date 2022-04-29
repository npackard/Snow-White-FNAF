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
        if (PlayerPrefs.GetInt("DayCount") == 0) { gameObject.SetActive(false); return; }

        audio = GetComponent<AudioSource>();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();

        bool key1 = PlayerPrefs.GetInt("Key1") == 1;
        bool key2 = PlayerPrefs.GetInt("Key2") == 1;
        bool key3 = PlayerPrefs.GetInt("Key3") == 1;
        bool key4 = PlayerPrefs.GetInt("Key4") == 1;

        gameObject.SetActive(false);
        if (keyIndex == 1 && !key1) gameObject.SetActive(true);
        if (keyIndex == 2 && !key2) gameObject.SetActive(true);
        if ((keyIndex == 3 && !key3 || keyIndex == 4 && !key4) && key1 && key2) gameObject.SetActive(true);
    }

    public void CollectKey()
    {
        audio.PlayOneShot(keyClip);
        mr.enabled = false;
        bc.enabled = false;

        DayUIManager.instance.CollectKey(keyIndex - 1);
        DayGameManager.instance.activeKeys--;
        PlayerPrefs.SetInt("Key" + keyIndex.ToString(), 1);
        DayGameManager.instance.GetKey(keyIndex);

        if (keyIndex == 5) DayUIManager.instance.UpdateToMineText();
    }
}
