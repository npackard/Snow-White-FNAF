using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayGemstone : MonoBehaviour
{
    public int gemIndex;

    public GameObject nextGemstone;

    public AudioClip gemClip;
    private AudioSource audio;

    private MeshRenderer mr;
    private BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();

        if (SceneManager.GetActiveScene().buildIndex == 3) return;

        if (PlayerPrefs.GetInt("DayCount") == 0 && gemIndex == 0) { return; }
        if (PlayerPrefs.GetInt("DayCount") == 0 && gemIndex != 0) { gameObject.SetActive(false); return; }
        if (PlayerPrefs.GetInt("DayCount") != 0 && gemIndex == 0) { gameObject.SetActive(false); return; }

        bool gem1 = PlayerPrefs.GetInt("Gem1") == 1;
        bool gem2 = PlayerPrefs.GetInt("Gem2") == 1;
        bool gem3 = PlayerPrefs.GetInt("Gem3") == 1;
        bool gem4 = PlayerPrefs.GetInt("Gem4") == 1;
        bool gem5 = PlayerPrefs.GetInt("Gem5") == 1;
        bool gem6 = PlayerPrefs.GetInt("Gem6") == 1;

        gameObject.SetActive(false);
        if (gemIndex == 1 && !gem1) gameObject.SetActive(true);
        if (gemIndex == 2 && !gem2) gameObject.SetActive(true);
        if (gemIndex == 3 && !gem3) gameObject.SetActive(true);
        if (gemIndex == 4 && !gem4) gameObject.SetActive(true);
        if (gemIndex == 5 && !gem5) gameObject.SetActive(true);
        if (gemIndex == 6 && !gem6) gameObject.SetActive(true);
    }

    public void CollectGem()
    {
        audio.PlayOneShot(gemClip);
        mr.enabled = false;
        bc.enabled = false;

        DayUIManager.instance.CollectGem(gemIndex);
        DayGameManager.instance.activeGems--;
        if (gemIndex == 0) DayGameManager.instance.FirstDayCollected();
        PlayerPrefs.SetInt("Gem" + gemIndex.ToString(), 1);

        if(nextGemstone) nextGemstone.SetActive(true);
    }
}
