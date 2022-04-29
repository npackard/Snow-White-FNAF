using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightDopey : MonoBehaviour
{
    public Animator anim;

    private bool fire;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Home() {
        fire = false;
        anim.SetInteger("position", 0);
    }

    public void Fire() {
        fire = true;
        anim.SetInteger("position", 1);
    }

    public void Door() {
        fire = false;
        anim.SetBool("door", true);
        anim.SetInteger("position", 2);
    }

    public void Vent() {
        fire = false;
        anim.SetInteger("position", 3);
    }

    public bool AtFire() {
        return fire;
    }
}
