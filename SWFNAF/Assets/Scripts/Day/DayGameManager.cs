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

    private bool[] doorKeys;
    public void GetKey(int i) { doorKeys[i] = true; }
    public bool CheckKey(int i) { return doorKeys[i]; }

    private bool[] dwarfGems;
    public void GetGem(int i) { if (!dwarfGems[i]) { dwarfGems[i] = true; gemCount++; } }
    public int gemCount = 1;

    private int inGameTime = 0;
    private float realTime = 0;
    private float unitTime = 10; // 15 min
    private float maxTime = 48; // 12 hours

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
    }

    private void FixedUpdate()
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
                    return;
                }
                DayUIManager.instance.UpdateTime(inGameTime);
            }
        }
    }
}
