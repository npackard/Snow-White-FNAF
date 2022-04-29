using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject panelEndDay;
    private Image panelEndDayImg;

    private bool darkerAnim = false;
    private bool maxOpac = false;
    private bool brighterAnim = false;
    private bool minOpac = false;

    private void Start()
    {
        panelEndDay.SetActive(false);
        panelEndDayImg = panelEndDay.GetComponent<Image>();

        BrighterAnim(1);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (darkerAnim && !maxOpac)
        {
            panelEndDayImg.color = new Color(panelEndDayImg.color.r, panelEndDayImg.color.g, panelEndDayImg.color.b, panelEndDayImg.color.a + 0.005f);

            if (panelEndDayImg.color.a >= 1.0f) maxOpac = true;
            return;
        }
        if (brighterAnim && !minOpac)
        {
            panelEndDayImg.color = new Color(panelEndDayImg.color.r, panelEndDayImg.color.g, panelEndDayImg.color.b, panelEndDayImg.color.a - 0.005f);

            if (panelEndDayImg.color.a <= 0f)
            {
                minOpac = true;
                panelEndDay.SetActive(false);
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            DarkerAnim();
            GameManager.instance.LoadMainMenu();
        }
    }

    // synonymous to starting day
    public void BrighterAnim(int time)
    {
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
        panelEndDayImg.color = new Color(panelEndDayImg.color.r, panelEndDayImg.color.g, panelEndDayImg.color.b, 0);

        darkerAnim = true;
        maxOpac = false;

        panelEndDay.SetActive(true);
    }
}
