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

    public GameObject lastKey;

    public int activeKeys = 0;
    public int activeGems = 0;

    public int inGameTime = 0;
    private float realTime = 0;
    public float unitTime = 3; // 15 min
    public int maxTime = 40; // linearly interpolated between 1 ~ 10 hours based on "energy"

    public bool day0Done = false;
    private int firstDayItems = 4; // map, mirror, gem

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        // for testing purposes
        /*PlayerPrefs.SetInt("IsNight", 0);
        PlayerPrefs.SetInt("DayCount", 1);
        PlayerPrefs.SetFloat("Energy", 50);
        PlayerPrefs.SetInt("Key1", 1);
        PlayerPrefs.SetInt("Key2", 1);
        PlayerPrefs.SetInt("Key3", 1);
        PlayerPrefs.SetInt("Key4", 1);
        PlayerPrefs.SetInt("Key5", 1);
        PlayerPrefs.SetInt("Gem1", 1);
        PlayerPrefs.SetInt("Gem2", 1);
        PlayerPrefs.SetInt("Gem3", 1);
        PlayerPrefs.SetInt("Gem4", 1);
        PlayerPrefs.SetInt("Gem5", 1);
        PlayerPrefs.SetInt("Gem6", 1);
        PlayerPrefs.SetInt("DwarfFree3", 0);
        PlayerPrefs.SetInt("DwarfFree4", 0);
        PlayerPrefs.SetInt("DwarfFree5", 0);
        PlayerPrefs.SetInt("DwarfFree6", 0);*/
        // end
    }

    // Start is called before the first frame update
    void Start()
    {
        doorKeys = new bool[7];
        doorKeys[0] = true;
        doorKeys[1] = PlayerPrefs.GetInt("Key1") == 1;
        doorKeys[2] = PlayerPrefs.GetInt("Key2") == 1;
        doorKeys[3] = PlayerPrefs.GetInt("Key3") == 1;
        doorKeys[4] = PlayerPrefs.GetInt("Key4") == 1;
        doorKeys[5] = PlayerPrefs.GetInt("Key5") == 1;
        doorKeys[6] = PlayerPrefs.GetInt("Key5") == 1;

        if (doorKeys[1])
        {
            if (doorKeys[2])
            {
                if (doorKeys[3])
                {
                    if (doorKeys[4])
                    {
                        activeKeys = 1; // key5
                        activeGems = 6;
                    } else
                    {
                        activeKeys = 1; // key4
                        activeGems = 6;
                    }
                } else
                {
                    activeKeys = 2; // key3,4
                    activeGems = 4;
                }
            } else
            {
                activeKeys = 1; // key2
                activeGems = 2;
            }
        } else
        {
            activeKeys = 2; //key1,2
            activeGems = 2;
        }

        if (PlayerPrefs.GetInt("Gem1") == 1) activeGems--;
        if (PlayerPrefs.GetInt("Gem2") == 1) activeGems--;
        if (PlayerPrefs.GetInt("Gem3") == 1) activeGems--;
        if (PlayerPrefs.GetInt("Gem4") == 1) activeGems--;
        if (PlayerPrefs.GetInt("Gem5") == 1) activeGems--;
        if (PlayerPrefs.GetInt("Gem6") == 1) activeGems--;

        inGameTime = (int) Mathf.Clamp((100 - PlayerPrefs.GetFloat("Energy")) / 10, 0, 9) * 4;

        if (PlayerPrefs.GetInt("Gem6") == 1) lastKey.SetActive(true);

        DayUIManager.instance.UpdateTime(inGameTime);
    }

    private void Update()
    {
        if (gettingBrighter)
        {
            StartCoroutine(Brighter());
            gettingBrighter = false;
        }

        if (PlayerPrefs.GetInt("DayCount") == 0 && day0Done) DayUIManager.instance.CollectiblesDone();
        else if (PlayerPrefs.GetInt("DayCount") != 0 && activeGems == 0 && activeKeys == 0) DayUIManager.instance.CollectiblesDone();
    }

    private void FixedUpdate()
    {
        if (!gettingBrighter && PlayerPrefs.GetInt("DayCount") != 0)
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

    public void FirstDayCollected()
    {
        firstDayItems--;
        if (firstDayItems == 0) day0Done = true;
    }

    private IEnumerator Brighter()
    {
        int time = 2;
        DayUIManager.instance.BrighterAnim(time);

        yield return new WaitForSeconds(time + 1);
    }

    public void GameEnding()
    {
        DayUIManager.instance.DarkerAnim();

        // placeholder if we want to exit through front door
        GameManager.instance.LoadMainMenu();
    }
}
