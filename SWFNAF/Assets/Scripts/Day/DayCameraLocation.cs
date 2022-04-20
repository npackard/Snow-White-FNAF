using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCameraLocation : MonoBehaviour
{
    public GameObject thisLoc;
    public string roomName;

    public GameObject[] nearbyCams;
    public GameObject[] dayDwarves;

    private void Start()
    {
        //gameObject.SetActive(false);
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
            dwarf.GetComponent<DayDwarf>().canBeSeen = true;
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
            dwarf.GetComponent<DayDwarf>().canBeSeen = false;
        }
    }
}
