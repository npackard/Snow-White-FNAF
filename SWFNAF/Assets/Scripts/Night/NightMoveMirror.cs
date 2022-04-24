using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMoveMirror : MonoBehaviour
{
    public static NightMoveMirror S;

    private Animator anim;

    private void Awake() {
        if (NightMoveMirror.S) {
            Destroy(this.gameObject);
        } else {
            S = this;
        }
    }

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
        NightGameManager.S.MirrorDown();
        anim.SetBool("down", true);
    }

    public void MirrorUp() {
        NightGameManager.S.MirrorUp();
        anim.SetBool("down", false);
    }
}
