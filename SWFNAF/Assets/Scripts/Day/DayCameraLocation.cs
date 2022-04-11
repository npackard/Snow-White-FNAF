using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCameraLocation : MonoBehaviour
{
    public DayCameraLocation left;
    public DayCameraLocation right;
    public DayCameraLocation front;
    public DayCameraLocation back;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject frontArrow;
    public GameObject backArrow;

    //public Vector2 viewAnglesX;
    //public Vector2 viewAnglesY;

    private void Start()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        frontArrow.SetActive(false);
        backArrow.SetActive(false);
    }

    public void OnThisCam()
    {
        if (left) leftArrow.SetActive(true);
        if (right) rightArrow.SetActive(true);
        if (front) frontArrow.SetActive(true);
        if (back) backArrow.SetActive(true);
    }

    public void LeaveThisCam()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        frontArrow.SetActive(false);
        backArrow.SetActive(false);
    }
}
