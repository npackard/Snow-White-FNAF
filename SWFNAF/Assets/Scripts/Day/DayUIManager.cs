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

    public void PanelInteractableOn()
    {
        panelInteractable.SetActive(true);
    }

    public void PanelInteractableOff()
    {
        panelInteractable.SetActive(false);
    }
}
