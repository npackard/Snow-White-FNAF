using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLookAtMirror : MonoBehaviour
{
    public GameObject mirror;

    private bool down = true; // down == not visible

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            down = !down;
            if (down) {
                mirror.GetComponent<NightMoveMirror>().MirrorDown();
            } else {
                mirror.GetComponent<NightMoveMirror>().MirrorUp();
            }
        }
    }
}
