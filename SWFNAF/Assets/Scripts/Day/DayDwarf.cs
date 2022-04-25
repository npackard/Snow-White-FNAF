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

    public AudioClip dwarfScareClip;
    public AudioClip dwarfGoneClip;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canBeSeen)
        {
            Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
            onScreen = screenPoint.z > 0 && screenPoint.x > -0.2 && 
                screenPoint.x < 1.2 && screenPoint.y > -0.2 && screenPoint.y < 1.2;

            if (onScreen && !seen)
            {
                // play creepy sound
                seen = true;
                PlayerPrefs.SetInt("DwarfFree" + dwarfIndex.ToString(), 1);
                audio.PlayOneShot(dwarfScareClip);

            }

            if (seen && !onScreen)
            {
                audio.PlayOneShot(dwarfGoneClip);
                Destroy(this.gameObject);
            }
        }
    }
}