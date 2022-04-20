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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void EndNight() {
        // move to daytime
        StartCoroutine(EndNightCor());
    }

    private IEnumerator EndNightCor()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

    public void EndDay()
    {
        // move to nighttime
        DayUIManager.instance.DarkerAnim();
        StartCoroutine(EndDayCor());
    }

    private IEnumerator EndDayCor()
    {
        PlayerPrefs.SetInt("DayCount", PlayerPrefs.GetInt("DayCount") + 1);
        PlayerPrefs.SetFloat("Energy", DayGameManager.instance.maxTime - DayGameManager.instance.inGameTime);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
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
}
