using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayFirstInteractable : MonoBehaviour
{
    public bool instructions = false;
    public Image instructionsImage;
    public AudioClip collectClip;
    private AudioSource audio;
    private BoxCollider bc;
    public GameObject mesh;

    // Start is called before the first frame update
    void Start()
    {
        if (instructions) instructionsImage.enabled = false;
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
        if (instructions) instructionsImage.enabled = true;
    }
}
