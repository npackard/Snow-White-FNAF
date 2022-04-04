using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightGameManager : MonoBehaviour
{
    public static NightGameManager S;

    private int night;

    private void Awake() {
        if (NightGameManager.S) {
            Destroy(this.gameObject);
        } else {
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetNight() {
        return night;
    }

    // indicates which night is next
    public void NightEnd() {
        night++;
    }
}
