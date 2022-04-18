using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCameraLocation : MonoBehaviour
{
    public GameObject thisLoc;
    public string roomName;

    public GameObject[] nearbyCams;

    private void Start()
    {
        thisLoc.SetActive(true);
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
    }
}
