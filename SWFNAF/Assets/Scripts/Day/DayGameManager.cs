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

    private bool[] dwarfGems;
    public void GetGem(int i) { if (!dwarfGems[i]) { dwarfGems[i] = true; gemCount++; } }
    public int gemCount = 1;

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
        dwarfGems = new bool[6];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
