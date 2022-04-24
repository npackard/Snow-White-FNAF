using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightMapSelector : MonoBehaviour
{
    public Image image;

    public Sprite basic;
    public Sprite study;
    public Sprite bathroom;
    public Sprite bedroom;
    public Sprite workshop;
    public Sprite all;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Key5") == 1) {
            image.sprite = all;
        } else if (PlayerPrefs.GetInt("Key4") == 1) {
            image.sprite = workshop;
        } else if (PlayerPrefs.GetInt("Key3") == 1) {
            image.sprite = bedroom;
        } else if (PlayerPrefs.GetInt("Key2") == 1) {
            image.sprite = bathroom;
        } else if (PlayerPrefs.GetInt("Key1") == 1) {
            image.sprite = study;
        } else {
            image.sprite = basic;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
