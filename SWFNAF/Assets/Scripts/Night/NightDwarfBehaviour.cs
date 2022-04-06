using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dwarf{dopey, sleepy, bashful, doc, sneezy, happy, grunmpy};
public enum Location{bathroom, dwarfBedroom, hall, kitchen, meatGrinders, mines, study, workSpace, snowWhiteBedroom, none};

public class NightDwarfBehaviour : MonoBehaviour
{
    [Range(1, 20)]
    public int difficultAdjustment = 1;
    [Range(1, 3)]
    public int introNight = 1;

    public int easyChance = 5;
    public int mediumChance = 7;
    public int hardChance = 10;

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
    private bool onCamera = false;
    private float moveTimer = 0f;
    private int shortWait = 25;
    private int longWait = 50;
    private int locationIndex = 0;

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
                    StartCoroutine(DopeyBehaviour());
                    break;
                case Dwarf.sleepy:
                    StartCoroutine(SleepyBehaviour());
                    break;
                case Dwarf.bashful:
                    StartCoroutine(BashfulBehaviour());
                    break;
                case Dwarf.doc:
                    StartCoroutine(DocBehaviour());
                    break;
                case Dwarf.sneezy:
                    StartCoroutine(SneezyBehaviour());
                    break;
                case Dwarf.happy:
                    StartCoroutine(HappyBehaviour());
                    break;
                case Dwarf.grunmpy:
                    StartCoroutine(GrumpyBehaviour());
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

    // gets a knife while sleepwalking, arrives via stairs
    private IEnumerator SleepyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // don't do anything if on camera
        if (location != NightGameManager.S.GetCamLocation()) {
            // prioritize attacking if in Snow White's bedroom
            if (location == Location.snowWhiteBedroom) {
                // attack (need logic for if stopped)
                // if (stopped) reset location
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, easy chance
                if (chance < (difficultAdjustment / 100f) * easyChance) {
                    location = sleepyPath[locationIndex + 1];
                    locationIndex++;
                }
                // give knife if in kitchen or further along path
            }
        }
    }

    // stays off camera as much as possible, arrives via fireplace
    private IEnumerator BashfulBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // don't do anything if off camera
        if (location != NightGameManager.S.GetCamLocation()) {
            if (location == Location.snowWhiteBedroom) {
                // attack (need logic for if stopped)
                // if (stopped) reset location
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, easy chance
                if (chance < (difficultAdjustment / 100f) * easyChance) {
                    location = sleepyPath[locationIndex + 1];
                    locationIndex++;
                }
            }
        }
    }

    private IEnumerator DocBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // don't do anything if off camera
        if (location != NightGameManager.S.GetCamLocation()) {
            if (location == Location.snowWhiteBedroom) {
                // attack (need logic for if stopped)
                // if (stopped) reset location
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, medium chance
                if (chance < (difficultAdjustment / 100f) * mediumChance) {
                    location = sleepyPath[locationIndex + 1];
                    locationIndex++;
                }
            }
        }
    }

    private IEnumerator SneezyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // don't do anything if off camera
        if (location != NightGameManager.S.GetCamLocation()) {
            if (location == Location.snowWhiteBedroom) {
                // attack (need logic for if stopped)
                // if (stopped) reset location
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, medium chance
                if (chance < (difficultAdjustment / 100f) * mediumChance) {
                    location = sleepyPath[locationIndex + 1];
                    locationIndex++;
                }
            }
        }
    }

    private IEnumerator HappyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // don't do anything if off camera
        if (location != NightGameManager.S.GetCamLocation()) {
            if (location == Location.snowWhiteBedroom) {
                // attack (need logic for if stopped)
                // if (stopped) reset location
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, hard chance
                if (chance < (difficultAdjustment / 100f) * hardChance) {
                    location = sleepyPath[locationIndex + 1];
                    locationIndex++;
                }
            }
        }
    }

    private IEnumerator GrumpyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // don't do anything if off camera
        if (location != NightGameManager.S.GetCamLocation()) {
            if (location == Location.snowWhiteBedroom) {
                // attack (need logic for if stopped)
                // if (stopped) reset location
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, hard chance
                if (chance < (difficultAdjustment / 100f) * hardChance) {
                    location = sleepyPath[locationIndex + 1];
                    locationIndex++;
                }
            }
        }
    }
}
