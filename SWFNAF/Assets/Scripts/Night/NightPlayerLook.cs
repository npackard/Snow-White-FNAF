using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightPlayerLook : MonoBehaviour
{
    [Range(0, 1)]
    public float mouseSensitivity = 1f;
    public float smoothing = 2f;

    public float minY = -30f;
    public float maxY = .0f;

    private float change;
    private float smooth;
    private float mouse;

    private bool leftTrigger = false;
    private bool rightTrigger = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftTrigger) {
            anim.ResetTrigger("lookLeft");
            leftTrigger = false;
        }
        if (rightTrigger) {
            anim.ResetTrigger("lookRight");
            rightTrigger = false;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            anim.SetTrigger("lookLeft");
            leftTrigger = true;
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            anim.SetTrigger("lookRight");
            rightTrigger = true;
        }
    }
}
