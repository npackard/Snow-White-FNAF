using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayDwarf : MonoBehaviour
{
    public bool canBeSeen = false;
    public Camera cam;

    private bool onScreen = false;
    private bool seen = false;

    private void Update()
    {
        if (canBeSeen)
        {
            Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
            onScreen = screenPoint.z > 0 && screenPoint.x > -0.2 && 
                screenPoint.x < 1.2 && screenPoint.y > -0.2 && screenPoint.y < 1.2;

            if (onScreen)
            {
                // play creepy sound
                seen = true;
            }

            if (seen && !onScreen) Destroy(this.gameObject);
        }
    }
}