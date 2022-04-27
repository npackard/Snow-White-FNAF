using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMirror : MonoBehaviour
{
    public void InteractMirror()
    {
        gameObject.tag = "Untagged";

        StartCoroutine(Darker());
    }
    private IEnumerator Darker()
    {
        MineUIManager.instance.DarkerAnim();

        yield return new WaitForSeconds(1);

        GameManager.instance.LoadMainMenu();
    }
}
