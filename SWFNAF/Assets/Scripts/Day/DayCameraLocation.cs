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

    public bool hasDoorLeft;
    public bool hasDoorRight;
    public bool hasDoorFront;
    public bool hasDoorBack;
    public bool openDoorLeft;
    public bool openDoorRight;
    public bool openDoorFront;
    public bool openDoorBack;

    private void Start()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        frontArrow.SetActive(false);
        backArrow.SetActive(false);
    }

    public void OpenDoor(int dir)
    {
        if (dir == 0) openDoorLeft = true;
        else if (dir == 1) openDoorRight = true;
        else if (dir == 2) openDoorFront = true;
        else if (dir == 3) openDoorBack = true;

        OnThisCam();
    }

    public void OnThisCam()
    {
        if (left && ((hasDoorLeft && openDoorLeft) || !hasDoorLeft)) leftArrow.SetActive(true);
        if (right && ((hasDoorRight && openDoorRight) || !hasDoorRight)) rightArrow.SetActive(true);
        if (front && ((hasDoorFront && openDoorFront) || !hasDoorFront)) frontArrow.SetActive(true);
        if (back && ((hasDoorBack && openDoorBack) || !hasDoorBack)) backArrow.SetActive(true);
    }

    public void LeaveThisCam()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        frontArrow.SetActive(false);
        backArrow.SetActive(false);
    }
}
