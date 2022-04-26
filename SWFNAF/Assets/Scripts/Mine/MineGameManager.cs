using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineGameManager : MonoBehaviour
{
    public static MineGameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<MineGameManager>();
            }

            return m_instance;
        }
    }

    public static MineGameManager m_instance;

    public GameObject mirror;

    private bool gettingBrighter = true;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mirror.SetActive(false);
    }

    private void Update()
    {
        if (gettingBrighter)
        {
            StartCoroutine(Brighter());
            gettingBrighter = false;
        }
    }

    public void AllGems()
    {
        // activate mirror
        mirror.SetActive(true);
    }

    private IEnumerator Brighter()
    {
        int time = 2;
        MineUIManager.instance.BrighterAnim(time);

        yield return new WaitForSeconds(time + 1);
    }
}
