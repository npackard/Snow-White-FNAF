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

    public GameObject shouldSleepText;

    public GameObject panelInteractable;
    public GameObject panelEndDay;
    public GameObject dayCount;
    public Text dayCountText;
    private Image panelEndDayImg;
    public GameObject timeTextObject;
    public Text timeText;
    public GameObject imageAim;

    private bool darkerAnim = false;
    private bool maxOpac = false;
    private bool brighterAnim = false;
    private bool minOpac = false;

    public GameObject[] gemSprites;
    public GameObject[] keySprites;

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
        dayCount.SetActive(false);
        panelEndDayImg = panelEndDay.GetComponent<Image>();
        imageAim.SetActive(false);

        if (PlayerPrefs.GetInt("DayCount") == 0) timeTextObject.SetActive(false);
        if (PlayerPrefs.GetInt("Key5") == 1) UpdateToMineText();

        if (PlayerPrefs.GetInt("Gem0") == 1) gemSprites[0].SetActive(true);
        if (PlayerPrefs.GetInt("Gem1") == 1) gemSprites[1].SetActive(true);
        if (PlayerPrefs.GetInt("Gem2") == 1) gemSprites[2].SetActive(true);
        if (PlayerPrefs.GetInt("Gem3") == 1) gemSprites[3].SetActive(true);
        if (PlayerPrefs.GetInt("Gem4") == 1) gemSprites[4].SetActive(true);
        if (PlayerPrefs.GetInt("Gem5") == 1) gemSprites[5].SetActive(true);
        if (PlayerPrefs.GetInt("Gem6") == 1) gemSprites[6].SetActive(true);

        if (PlayerPrefs.GetInt("Key1") == 1) keySprites[0].SetActive(true);
        if (PlayerPrefs.GetInt("Key2") == 1) keySprites[1].SetActive(true);
        if (PlayerPrefs.GetInt("Key3") == 1) keySprites[2].SetActive(true);
        if (PlayerPrefs.GetInt("Key4") == 1) keySprites[3].SetActive(true);
        if (PlayerPrefs.GetInt("Key5") == 1) keySprites[4].SetActive(true);
    }

    private void Update()
    {
        if (darkerAnim && !maxOpac)
        {
            panelEndDayImg.color = new Color(panelEndDayImg.color.r, panelEndDayImg.color.g, panelEndDayImg.color.b, panelEndDayImg.color.a + 0.005f);
            dayCountText.color = new Color(dayCountText.color.r, dayCountText.color.g, dayCountText.color.b, dayCountText.color.a + 0.005f);

            if (panelEndDayImg.color.a >= 1.0f) maxOpac = true;
        }
        if (brighterAnim && !minOpac)
        {
            panelEndDayImg.color = new Color(panelEndDayImg.color.r, panelEndDayImg.color.g, panelEndDayImg.color.b, panelEndDayImg.color.a - 0.005f);
            dayCountText.color = new Color(dayCountText.color.r, dayCountText.color.g, dayCountText.color.b, dayCountText.color.a - 0.005f);

            if (panelEndDayImg.color.a <= 0f)
            {
                minOpac = true;
                panelEndDay.SetActive(false);
                dayCount.SetActive(false);
                imageAim.SetActive(true);
            }
        }
    }

    public void UpdateTime(int t)
    {
        bool isPM = t / 4 > 3;
        string newTime = isPM ? ((t - 12) / 4).ToString() + ":" : (9 + t / 4).ToString() + ":";
        newTime += (t % 4 == 0) ? "00" : ((t % 4) * 15).ToString();
        newTime += isPM ? " PM" : " AM";
        timeText.text = newTime;
    }

    public void CollectKey(int i)
    {
        keySprites[i].SetActive(true);
    }

    public void CollectGem(int i)
    {
        gemSprites[i].SetActive(true);
    }

    public void CollectiblesDone()
    {
        shouldSleepText.SetActive(true);
    }

    public void UpdateToMineText()
    {
        shouldSleepText.GetComponent<Text>().text = "I am ready now...";
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

        dayCountText.text = "Day " + (PlayerPrefs.GetInt("DayCount")).ToString();

        panelEndDay.SetActive(true);
        if (PlayerPrefs.GetInt("DayCount") != 0) dayCount.SetActive(true);

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
        dayCount.SetActive(false);

        panelEndDayImg.color = new Color(panelEndDayImg.color.r, panelEndDayImg.color.g, panelEndDayImg.color.b, 0);

        darkerAnim = true;
        maxOpac = false;

        panelEndDay.SetActive(true);
    }
}
