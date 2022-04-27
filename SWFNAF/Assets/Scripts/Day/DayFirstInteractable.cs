using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayFirstInteractable : MonoBehaviour
{
    public AudioClip collectClip;
    private AudioSource audio;
    private BoxCollider bc;
    public GameObject mesh;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider>();
        if (PlayerPrefs.GetInt("DayCount") != 0) gameObject.SetActive(false);
    }

    public void Interact()
    {
        DayGameManager.instance.FirstDayCollected();
        audio.PlayOneShot(collectClip);
        bc.enabled = false;
        mesh.SetActive(false);
    }
}
