using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSleep : MonoBehaviour
{
    private bool canControl = true;
    private float timer = 0f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl) {
            if (Input.GetKey(KeyCode.E)) {
                timer += Time.deltaTime;
                anim.SetBool("open", false);
                NightGameManager.S.CloseEyes();
            } else {
                timer = 0f;
                anim.SetBool("open", true);
                NightGameManager.S.OpenEyes();
            }

            if (timer > 1f) {
                AddEnergy();
                timer = 0f;
            }
        } else {
            anim.SetBool("open", true);
            NightGameManager.S.OpenEyes();
        }
    }

    private void AddEnergy() {
        NightGameManager.S.AddEnergy(1f);
    }

    public void YesControl() {
        canControl = true;
    }

    public void NoControl() {
        canControl = false;
    }
}
