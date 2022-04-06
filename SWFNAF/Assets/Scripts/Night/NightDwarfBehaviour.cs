using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dwarf{dopey, sleepy, bashful, doc, sneezy, happy, grunmpy};
public enum Location{bathroom, dwarfBedroom, hall, kitchen, meatGrinders, mines, workSpace, snowWhiteBedroom};

public class NightDwarfBehaviour : MonoBehaviour
{
    [Range(1, 20)]
    public int difficultAdjustment = 1;
    [Range(1, 3)]
    public int introNight = 1;

    public Dwarf dwarf;

    public Location[] dopeyPath;
    public Location[] sleepyPath;
    public Location[] bashfulPath;
    public Location[] docPath;
    public Location[] sneezyPath;
    public Location[] happyPath;
    public Location[] grumpyPath;

    private bool isActive = false;
    private bool isEnabled = false;
    private float moveTimer = 0f;
    private int shortWait = 25;
    private int longWait = 50;

    private Location location;
    
    // Start is called before the first frame update
    void Start()
    {
        location = Location.dwarfBedroom;
        SetTimes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTimes() {
        shortWait = (int)(shortWait / difficultAdjustment);
        longWait = (int)(longWait / difficultAdjustment);
    }

    public void ResetDwarf() {
        isActive = false;
        isEnabled = false;
        StopAllCoroutines();
        location = Location.dwarfBedroom;
    }

    public void StartMoving() {
        if (NightGameManager.S.GetNight() >= introNight) {
            isEnabled = true;
            switch(dwarf) {
                case Dwarf.dopey:
                    DopeyBehaviour();
                    break;
                case Dwarf.sleepy:
                    SleepyBehaviour();
                    break;
                case Dwarf.bashful:
                    BashfulBehaviour();
                    break;
                case Dwarf.doc:
                    DocBehaviour();
                    break;
                case Dwarf.sneezy:
                    SneezyBehaviour();
                    break;
                case Dwarf.happy:
                    HappyBehaviour();
                    break;
                case Dwarf.grunmpy:
                    GrumpyBehaviour();
                    break;
                default:
                    Debug.Log("you did something wrong");
                    break;
                }
            }
    }

    private int GetWaitTime() {
        return (int)Random.Range(shortWait, longWait);
    }

    private IEnumerator DopeyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
    }

    private IEnumerator SleepyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
    }

    private IEnumerator BashfulBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
    }

    private IEnumerator DocBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
    }

    private IEnumerator SneezyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
    }

    private IEnumerator HappyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
    }

    private IEnumerator GrumpyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
    }
}
