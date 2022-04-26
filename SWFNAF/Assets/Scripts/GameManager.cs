using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }

    public static GameManager m_instance;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void EndNight() {
        // move to daytime
        StartCoroutine(EndNightCor());
    }

    private IEnumerator EndNightCor()
    {
        PlayerPrefs.SetInt("IsNight", 0);
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(4); // intermission scene
    }

    public void EndDay()
    {
        // move to nighttime
        DayUIManager.instance.DarkerAnim();
        StartCoroutine(EndDayCor());
    }

    private IEnumerator EndDayCor()
    {
        PlayerPrefs.SetInt("IsNight", 1);
        PlayerPrefs.SetInt("DayCount", PlayerPrefs.GetInt("DayCount") + 1);
        PlayerPrefs.SetFloat("Energy", DayGameManager.instance.maxTime - DayGameManager.instance.inGameTime);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(4); // intermission scene
    }

    public void EndIntermission()
    {
        StartCoroutine(EndIntermissionCor());
    }

    private IEnumerator EndIntermissionCor()
    {
        yield return new WaitForSeconds(0);
        if (PlayerPrefs.GetInt("IsNight") == 0) SceneManager.LoadScene(2); // load Day
        else SceneManager.LoadScene(1); // load Night
    }

    public void LoadMine()
    {
        DayUIManager.instance.DarkerAnim();
        StartCoroutine(LoadMineCor());
    }

    private IEnumerator LoadMineCor()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
