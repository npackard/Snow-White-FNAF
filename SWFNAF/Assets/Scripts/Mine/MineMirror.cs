using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMirror : MonoBehaviour
{
    private SkinnedMeshRenderer smr;
    public SkinnedMeshRenderer mirrorFrameRenderer;
    private AudioSource audio;
    public Material brokenMirror;
    public AudioClip glassCrack;

    private bool interactedOnce = false;

    private void Start()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        audio = GetComponent<AudioSource>();
    }

    public void InteractMirror()
    {
        if (!interactedOnce)
        {
            smr.material = brokenMirror;
            mirrorFrameRenderer.material = brokenMirror;
            audio.PlayOneShot(glassCrack);
            interactedOnce = true;
        }
        else
        {
            gameObject.tag = "Untagged";

            MineUIManager.instance.DarkerAnim();
            PlayerPrefs.SetInt("Key6", 1);
            GameManager.instance.LoadMainMenu();
        }
    }
}
