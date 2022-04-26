using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineUIManager : MonoBehaviour
{
    public static MineUIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<MineUIManager>();
            }

            return m_instance;
        }
    }

    public static MineUIManager m_instance;

    public GameObject panelInteractable;
    public GameObject panelEndDay;
    private Image panelEndDayImg;
    public GameObject imageAim;

    private bool darkerAnim = false;
    private bool maxOpac = false;
    private bool brighterAnim = false;
    private bool minOpac = false;

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
        panelEndDay.SetActive(false);
        panelEndDayImg = panelEndDay.GetComponent<Image>();
        imageAim.SetActive(false);
    }

    private void Update()
    {
        if (darkerAnim && !maxOpac)
        {
            panelEndDayImg.color = new Color(panelEndDayImg.color.r, panelEndDayImg.color.g, panelEndDayImg.color.b, panelEndDayImg.color.a + 0.005f);

            if (panelEndDayImg.color.a >= 1.0f) maxOpac = true;
        }
        if (brighterAnim && !minOpac)
        {
            panelEndDayImg.color = new Color(panelEndDayImg.color.r, panelEndDayImg.color.g, panelEndDayImg.color.b, panelEndDayImg.color.a - 0.005f);

            if (panelEndDayImg.color.a <= 0f)
            {
                minOpac = true;
                panelEndDay.SetActive(false);
                imageAim.SetActive(true);
            }
        }
    }

    public void PanelInteractableOn()
    {
        panelInteractable.SetActive(true);
    }

    public void PanelInteractableOff()
    {
        panelInteractable.SetActive(false);
    }

    // synonymous to starting day
    public void BrighterAnim(int time)
    {
        imageAim.SetActive(false);

        panelEndDayImg.color = new Color(panelEndDayImg.color.r, panelEndDayImg.color.g, panelEndDayImg.color.b, 1);

        panelEndDay.SetActive(true);

        StartCoroutine(WaitOneSecBrighter(time));
    }

    private IEnumerator WaitOneSecBrighter(int time)
    {
        yield return new WaitForSeconds(time);

        brighterAnim = true;
        minOpac = false;
    }

    // synonymous to ending day
    public void DarkerAnim()
    {
        imageAim.SetActive(false);

        panelEndDayImg.color = new Color(panelEndDayImg.color.r, panelEndDayImg.color.g, panelEndDayImg.color.b, 0);

        darkerAnim = true;
        maxOpac = false;

        panelEndDay.SetActive(true);
    }
}
