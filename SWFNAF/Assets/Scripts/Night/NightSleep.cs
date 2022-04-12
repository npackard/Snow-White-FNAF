using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSleep : MonoBehaviour
{
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
        if (Input.GetKey(KeyCode.E)) {
            timer += Time.deltaTime;
            anim.SetBool("open", false);
        } else {
            timer = 0f;
            anim.SetBool("open", true);
        }

        if (timer > 1f) {
            AddEnergy();
            timer = 0f;
        }
    }

    private void AddEnergy() {
        NightGameManager.S.AddEnergy(1f);
    }
}
