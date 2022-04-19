using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayGemstone : MonoBehaviour
{
    public int gemIndex;

    // Start is called before the first frame update
    void Start()
    {
        bool gem1 = PlayerPrefs.GetInt("Gem1") == 1;
        bool gem2 = PlayerPrefs.GetInt("Gem2") == 1;
        bool gem3 = PlayerPrefs.GetInt("Gem3") == 1;
        bool gem4 = PlayerPrefs.GetInt("Gem4") == 1;
        bool gem5 = PlayerPrefs.GetInt("Gem3") == 1;
        bool gem6 = PlayerPrefs.GetInt("Gem4") == 1;

        if (gemIndex == 1 && gem1) gameObject.SetActive(false);
        if (gemIndex == 2 && gem2) gameObject.SetActive(false);
        if (gemIndex == 3 && gem3) gameObject.SetActive(false);
        if (gemIndex == 4 && gem4) gameObject.SetActive(false);
        if (gemIndex == 5 && gem5) gameObject.SetActive(false);
        if (gemIndex == 6 && gem6) gameObject.SetActive(false);
    }
}
