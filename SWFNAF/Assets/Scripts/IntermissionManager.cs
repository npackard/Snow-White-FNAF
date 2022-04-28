using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntermissionManager : MonoBehaviour
{
    public Text surviveText;

    void Start()
    {
        if (PlayerPrefs.GetInt("DayCount") != 0)
        {
            if (PlayerPrefs.GetInt("IsNight") == 1)
            {
                surviveText.text = "Day " + PlayerPrefs.GetInt("DayCount") + " Clear";
            }
            else
            {
                surviveText.text = "Night " + PlayerPrefs.GetInt("DayCount") + " Clear";
            }
        } else
        {
            surviveText.text = "Intro Clear";
        }

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
