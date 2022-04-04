using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dwarf{dopey, sleepy, bashful, doc, sneezy, happy, grunmpy};

public class NightDwarfBehaviour : MonoBehaviour
{
    public int introNight;

    public Dwarf dwarf;

    private bool isActive = false;
    private float moveTimer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetDwarf() {
        isActive = false;
    }

    private void StartMoving() {
        if (NightGameManager.S.GetNight() >= introNight) {
            isActive = true;
            switch(dwarf) {
                case Dwarf.dopey:
                    break;
                case Dwarf.sleepy:
                    break;
                case Dwarf.bashful:
                    break;
                case Dwarf.doc:
                    break;
                case Dwarf.sneezy:
                    break;
                case Dwarf.happy:
                    break;
                case Dwarf.grunmpy:
                    break;
                default:
                    Debug.Log("you did something wrong");
                    break;
                }
            }
    }
}
