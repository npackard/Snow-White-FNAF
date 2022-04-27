using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMirror : MonoBehaviour
{
    public void InteractMirror()
    {
        gameObject.tag = "Untagged";

        MineUIManager.instance.DarkerAnim();
        PlayerPrefs.SetInt("Key6", 1);
        GameManager.instance.LoadMainMenu();
    }
}
