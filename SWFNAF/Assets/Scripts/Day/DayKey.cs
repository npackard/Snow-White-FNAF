using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayKey : MonoBehaviour
{
    // 0 means open by default
    // 1 -> study, 2 -> workshop, 3-> bathroom, 4 -> dwarf bedroom, 5 -> mining
    public int keyIndex;

    // Start is called before the first frame update
    void Start()
    {
        bool key1 = PlayerPrefs.GetInt("Key1") == 1;
        bool key2 = PlayerPrefs.GetInt("Key2") == 1;
        bool key3 = PlayerPrefs.GetInt("Key3") == 1;
        bool key4 = PlayerPrefs.GetInt("Key4") == 1;
        bool allKeys = key1 && key2 && key3 && key4;

        if ((keyIndex == 3 || keyIndex == 4) && key1 && key2) gameObject.SetActive(true);
        if (keyIndex == 5 && allKeys) gameObject.SetActive(true);

        if (keyIndex == 1 && key1) gameObject.SetActive(false);
        if (keyIndex == 2 && key2) gameObject.SetActive(false);
        if (keyIndex == 3 && key3) gameObject.SetActive(false);
        if (keyIndex == 4 && key4) gameObject.SetActive(false);
    }
}
