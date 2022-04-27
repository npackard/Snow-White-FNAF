using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCameraLocation : MonoBehaviour
{
    public GameObject thisLoc;
    public string roomName;

    public GameObject[] nearbyCams;
    public GameObject[] dayDwarves;

    public GameObject bedText;

    private void Start()
    {
        if (bedText && PlayerPrefs.GetInt("DayCount") != 0) bedText.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("CamLocations");
        gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("CamLocations");
    }

    public void OnThisCam()
    {
        thisLoc.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer(roomName);
        gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer(roomName);

        foreach (GameObject cam in nearbyCams)
        {
            cam.SetActive(true);
        }

        foreach (GameObject dwarf in dayDwarves)
        {
            if (dwarf) dwarf.GetComponent<DayDwarf>().canBeSeen = true;
        }

        if (bedText)
        {
            if (PlayerPrefs.GetInt("DayCount") == 0)
            {
                if (DayGameManager.instance.day0Done) bedText.SetActive(true);
            }
            else
            {
                bedText.SetActive(true);
            }
        }
    }

    public void LeaveThisCam()
    {
        thisLoc.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("CamLocations");
        gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("CamLocations");

        foreach (GameObject cam in nearbyCams)
        {
            cam.SetActive(false);
        }

        foreach (GameObject dwarf in dayDwarves)
        {
            if (dwarf) dwarf.GetComponent<DayDwarf>().canBeSeen = false;
        }

        if (bedText)
        {
            bedText.SetActive(false);
        }
    }
}
