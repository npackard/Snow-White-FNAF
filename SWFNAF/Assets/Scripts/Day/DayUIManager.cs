using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayUIManager : MonoBehaviour
{
    public static DayUIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<DayUIManager>();
            }

            return m_instance;
        }
    }

    public static DayUIManager m_instance;

    public GameObject panelInteractable;
    public Text timeText;

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
        panelInteractable.SetActive(false);
    }

    public void UpdateTime(int t)
    {
        bool isPM = t / 4 > 3;
        string newTime = isPM ? ((t - 12) / 4).ToString() + ":" : (9 + t / 4).ToString() + ":";
        newTime += (t % 4 == 0) ? "00" : ((t % 4) * 15).ToString();
        newTime += isPM ? " PM" : " AM";
        timeText.text = newTime;
    }

    public void PanelInteractableOn()
    {
        panelInteractable.SetActive(true);
    }

    public void PanelInteractableOff()
    {
        panelInteractable.SetActive(false);
    }
}
