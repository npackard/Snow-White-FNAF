using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePedestal : MonoBehaviour
{
    public AudioClip gemActivateClip;

    private MeshRenderer mr;
    private SphereCollider sc;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        sc = GetComponent<SphereCollider>();
        audio = GetComponent<AudioSource>();
    }

    public void ActivateGem()
    {
        mr.enabled = false;
        sc.enabled = false;
        audio.PlayOneShot(gemActivateClip);
    }
}
