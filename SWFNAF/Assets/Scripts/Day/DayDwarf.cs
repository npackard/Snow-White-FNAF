using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayDwarf : MonoBehaviour
{
    public bool canBeSeen = false;
    public Camera cam;

    public int dwarfIndex;

    private bool onScreen = false;
    private bool seen = false;

    public GameObject dwarfBody;
    public AudioClip dwarfScareClip;
    public AudioClip dwarfGoneClip;
    private AudioSource audio;
    private bool playedOnce = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("DwarfFree" + dwarfIndex.ToString()) == 1) gameObject.SetActive(false);
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canBeSeen)
        {
            Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
            onScreen = screenPoint.z > 0 && screenPoint.x > -0.4 && 
                screenPoint.x < 1.4 && screenPoint.y > -0.4 && screenPoint.y < 1.4;

            if (onScreen && !seen)
            {
                // play creepy sound
                seen = true;
                PlayerPrefs.SetInt("DwarfFree" + dwarfIndex.ToString(), 1);
                audio.PlayOneShot(dwarfScareClip);

            }

            if (seen && !onScreen && !playedOnce)
            {
                playedOnce = true;
                audio.PlayOneShot(dwarfGoneClip);
                dwarfBody.SetActive(false);
            }
        }
    }
}