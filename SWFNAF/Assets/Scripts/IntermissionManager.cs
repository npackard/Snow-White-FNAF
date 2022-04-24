using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class IntermissionManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnContinueBtnClick()
    {
        GameManager.instance.EndIntermission();
    }

    public void OnExitBtnClick()
    {
        GameManager.instance.LoadMainMenu();
    }
}
