using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMoveMirror : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MirrorDown() {
        anim.SetBool("down", true);
    }

    public void MirrorUp() {
        anim.SetBool("down", false);
    }
}
