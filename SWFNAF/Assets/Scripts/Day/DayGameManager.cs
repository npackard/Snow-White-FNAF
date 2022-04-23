using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayGameManager : MonoBehaviour
{
    public static DayGameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<DayGameManager>();
            }

            return m_instance;
        }
    }

    public static DayGameManager m_instance;

    private bool gettingBrighter = true;

    private bool[] doorKeys;
    public void GetKey(int i) { doorKeys[i] = true; }
    public bool CheckKey(int i) { return doorKeys[i]; }

    public int inGameTime = 0;
    private float realTime = 0;
    public float unitTime = 3; // 15 min
    public int maxTime = 40; // linearly interpolated between 1 ~ 10 hours based on "energy"

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        // for testing purposes
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
        // end
    }

    // Start is called before the first frame update
    void Start()
    {
        doorKeys = new bool[6];
        doorKeys[0] = true;
        doorKeys[1] = PlayerPrefs.GetInt("Key1") == 1;
        doorKeys[2] = PlayerPrefs.GetInt("Key2") == 1;
        doorKeys[3] = PlayerPrefs.GetInt("Key3") == 1;
        doorKeys[4] = PlayerPrefs.GetInt("Key4") == 1;
        doorKeys[5] = PlayerPrefs.GetInt("Key5") == 1;

        inGameTime = (int) Mathf.Clamp((100 - PlayerPrefs.GetFloat("energy")) / 10, 0, 9) * 4;

        DayUIManager.instance.UpdateTime(inGameTime);
    }

    private void Update()
    {
        if (gettingBrighter)
        {
            StartCoroutine(Brighter());
            gettingBrighter = false;
        }
    }

    private void FixedUpdate()
    {
        if (!gettingBrighter)
        {
            if (inGameTime <= maxTime)
            {
                realTime += Time.fixedDeltaTime;
                if (realTime >= unitTime)
                {
                    realTime = 0;
                    inGameTime += 1;
                    if (inGameTime > maxTime)
                    {
                        // switch to night time
                        GameManager.instance.EndDay();
                        return;
                    }
                    DayUIManager.instance.UpdateTime(inGameTime);
                }
            }
        }
    }

    private IEnumerator Brighter()
    {
        DayUIManager.instance.BrighterAnim();

        yield return new WaitForSeconds(1);
    }
}
