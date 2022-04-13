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

    private bool[] dwarfGems;
    public void GetGem(int i) { if (!dwarfGems[i]) { dwarfGems[i] = true; gemCount++; } }
    public int gemCount = 1;

    private int inGameTime = 0;
    private float realTime = 0;
    public float unitTime = 5; // 15 min
    private float maxTime = 2; // linearly interpolated between 1 ~ 10 hours based on "energy"

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        doorKeys = new bool[5];
        doorKeys[0] = true;

        dwarfGems = new bool[6];
        inGameTime = 0;
        DayUIManager.instance.UpdateTime(inGameTime);

        maxTime = Mathf.Clamp(PlayerPrefs.GetFloat("energy") / 10, 1, 10) * 4;
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
                        // update PlayerPrefs on the progress of gemstones
                        PlayerPrefs.SetInt("gemCount", gemCount);

                        // switch to night time
                        DayUIManager.instance.DarkerAnim();
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
