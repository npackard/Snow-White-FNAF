using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject btnContinue;

    private void Start()
    {
        if (PlayerPrefs.GetInt("DayCount") > 0) btnContinue.SetActive(true);
        else btnContinue.SetActive(false);
    }

    public void OnStartBtnClick()
    {
        // reset all playerprefs
        PlayerPrefs.SetInt("IsNight", 1);
        PlayerPrefs.SetInt("DayCount", 0);
        PlayerPrefs.SetFloat("Energy", 0);
        PlayerPrefs.SetInt("Key1", 0);
        PlayerPrefs.SetInt("Key2", 0);
        PlayerPrefs.SetInt("Key3", 0);
        PlayerPrefs.SetInt("Key4", 0);
        PlayerPrefs.SetInt("Key5", 0);
        PlayerPrefs.SetInt("Gem1", 0);
        PlayerPrefs.SetInt("Gem2", 0);
        PlayerPrefs.SetInt("Gem3", 0);
        PlayerPrefs.SetInt("Gem4", 0);
        PlayerPrefs.SetInt("Gem5", 0);
        PlayerPrefs.SetInt("Gem6", 0);
        PlayerPrefs.SetInt("DwarfFree3", 0);
        PlayerPrefs.SetInt("DwarfFree4", 0);
        PlayerPrefs.SetInt("DwarfFree5", 0);
        PlayerPrefs.SetInt("DwarfFree6", 0);
        PlayerPrefs.SetInt("night", 0);

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
