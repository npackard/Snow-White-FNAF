using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnStartBtnClick()
    {
        PlayerPrefs.SetInt("DayCount", 0);
        PlayerPrefs.SetFloat("Energy", 0);

        SceneManager.LoadScene(1);
    }

    public void OnContinueBtnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitBtnClick()
    {
        Application.Quit();
    }
}
