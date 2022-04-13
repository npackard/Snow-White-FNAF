using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Dwarf{dopey, sleepy, bashful, doc, sneezy, happy, grunmpy};
public enum Location{bathroom, dwarfBedroom, hall, kitchen, meatGrinders, mines, study, workspace, snowWhiteBedroom, none};

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
    public Transform deathPosition;

    public Location[] dopeyPath;
    public Location[] sleepyPath;
    public Location[] bashfulPath;
    public Location[] docPath;
    public Location[] sneezyPath;
    public Location[] happyPath;
    public Location[] grumpyPath;

    public Transform[] sleepyTransformPath;
    public Transform[] bashfulTransformPath;
    public Transform[] docTransformPath;
    public Transform[] sneezyTransformPath;
    public Transform[] happyTransformPath;
    public Transform[] grumpyTransformPath;

    private bool isActive = false;
    private bool isEnabled = false;
    private bool onCamera = false;
    private float finalDifficulty = 1f;
    private float moveTimer = 0f;
    private int shortWait = 25;
    private int longWait = 50;
    private int locationIndex = 0;

    private Location location;
    
    // Start is called before the first frame update
    void Start()
    {
        location = Location.dwarfBedroom;
        switch(dwarf) {
            case Dwarf.sleepy:
                transform.position = sleepyTransformPath[0].position;
                location = sleepyPath[0];
                break;
            case Dwarf.bashful:
                transform.position = bashfulTransformPath[0].position;
                location = bashfulPath[0];
                break;
            case Dwarf.doc:
                transform.position = docTransformPath[0].position;
                location = docPath[0];
                break;
            case Dwarf.sneezy:
                transform.position = sneezyTransformPath[0].position;
                location = sneezyPath[0];
                break;
            case Dwarf.happy:
                transform.position = happyTransformPath[0].position;
                location = happyPath[0];
                break;
            case Dwarf.grunmpy:
                transform.position = grumpyTransformPath[0].position;
                location = grumpyPath[0];
                break;
            default:
                Debug.Log("oh no");
                break;
        }
        SetTimes();
        SetFinalDifficulty();
        StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTimes() {
        shortWait = (int)(shortWait / difficultAdjustment);
        longWait = (int)(longWait / difficultAdjustment);
    }

    public void SetFinalDifficulty() {
        if (dwarf == Dwarf.sleepy || dwarf == Dwarf.bashful) {
            finalDifficulty = (difficultAdjustment / 100f) * easyChance;
        } else if (dwarf == Dwarf.doc || dwarf == Dwarf.sneezy) {
            finalDifficulty = (difficultAdjustment / 100f) * mediumChance;
        } else if (dwarf == Dwarf.happy || dwarf == Dwarf.grunmpy) {
            finalDifficulty = (difficultAdjustment / 100f) * hardChance;
        } else {
            // dopey
        }
    }

    public void ResetDwarf() {
        isActive = false;
        isEnabled = false;
        StopAllCoroutines();
        location = Location.dwarfBedroom;
        locationIndex = 0;
    }

    public void EntranceClosedReset() {
        StopAllCoroutines();
        location = Location.dwarfBedroom;
        locationIndex = 0;
        difficultAdjustment++;
        SetFinalDifficulty();
        StartMoving();
    }

    private IEnumerator Die() {
        NightGameManager.S.Die();
        yield return new WaitForSeconds(3f);
        NightGameManager.S.StartLevelAgain();
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
                    //StartCoroutine(DocBehaviour());
                    break;
                case Dwarf.sneezy:
                    //StartCoroutine(SneezyBehaviour());
                    break;
                case Dwarf.happy:
                    //StartCoroutine(HappyBehaviour());
                    break;
                case Dwarf.grunmpy:
                    //StartCoroutine(GrumpyBehaviour());
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
        float time = GetWaitTime();
        Debug.Log("Sleepy: " + time.ToString());
        yield return new WaitForSeconds(time);
        // prioritize attacking if in Snow White's bedroom
        if (location == Location.snowWhiteBedroom && NightGameManager.S.GetCamLocation() == Location.none) {
            if (NightGameManager.S.GetDoorClosed()) {
                location = sleepyPath[0];
                locationIndex = 0;
                transform.position = sleepyTransformPath[0].position;
            } else {
                if (NightGameManager.S.GetEyesClosed()) {
                    NightGameManager.S.eyes.NoControl();
                }
                transform.position = deathPosition.position;
                StartCoroutine(Die());
            }
        } else if (location == Location.snowWhiteBedroom) {
            location = sleepyPath[0];
            locationIndex = 0;
            transform.position = sleepyTransformPath[0].position;
        } else if (location != NightGameManager.S.GetCamLocation()) {
            float chance = Random.Range(0f, 1f);
            // move to next room in path, easy chance
            if (chance < finalDifficulty) {
                locationIndex++;
                location = sleepyPath[locationIndex];
                transform.position = sleepyTransformPath[locationIndex].position;
            }
        }
        StartCoroutine(SleepyBehaviour());
    }

    // stays off camera as much as possible, arrives via fireplace
    private IEnumerator BashfulBehaviour() {
        float time = GetWaitTime();
        Debug.Log("Bashful: " + time.ToString());
        yield return new WaitForSeconds(time);
        // prioritize attacking if in Snow White's bedroom
        if (location == Location.snowWhiteBedroom && NightGameManager.S.GetCamLocation() == Location.none) {
            if (NightGameManager.S.GetFireLit()) {
                location = bashfulPath[0];
                locationIndex = 0;
                transform.position = bashfulTransformPath[0].position;
            } else {
                transform.position = deathPosition.position;
                StartCoroutine(Die());
            }
        } else if (location == Location.snowWhiteBedroom) {
            location = bashfulPath[0];
            locationIndex = 0;
            transform.position = bashfulTransformPath[0].position;
        } else if (location != NightGameManager.S.GetCamLocation()) {
            float chance = Random.Range(0f, 1f);
            // move to next room in path, easy chance
            if (chance < finalDifficulty) {
                locationIndex++;
                location = bashfulPath[locationIndex];
                transform.position = bashfulTransformPath[locationIndex].position;
            }
        }
        StartCoroutine(BashfulBehaviour());
    }

//// NICOLE NEEDS TO CHANGE ALL OF THE FOLLOWING CODE TO MATCH SLEEPY AND BASHFUL BEHAVIOUR \\\\\

/*
    private IEnumerator DocBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // don't do anything if off camera
        if (location != NightGameManager.S.GetCamLocation()) {
            // prioritize attacking if in Snow White's bedroom
            if (location == Location.snowWhiteBedroom) {
                if (NightGameManager.S.GetDoorClosed()) {
                    location = sleepyPath[0];
                    locationIndex = 0;
                    transform.position = docTransformPath[0].position;
                } else {
                    // attack + player dies
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, medium chance
                if (chance < finalDifficulty) {
                    Move();
                    // physically move
                    transform.position = docTransformPath[locationIndex].position;
                }
                // give knife if in kitchen or further along path
            }
        }
        StartCoroutine(DocBehaviour());
    }

    private IEnumerator SneezyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // don't do anything if off camera
        if (location != NightGameManager.S.GetCamLocation()) {
            // prioritize attacking if in Snow White's bedroom
            if (location == Location.snowWhiteBedroom) {
                if (NightGameManager.S.GetDoorClosed()) {
                    location = sleepyPath[0];
                    locationIndex = 0;
                    transform.position = sneezyTransformPath[0].position;
                } else {
                    // attack + player dies
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, medium chance
                if (chance < finalDifficulty) {
                    Move();
                    // physically move
                    transform.position = sneezyTransformPath[locationIndex].position;
                }
                // give knife if in kitchen or further along path
            }
        }
        StartCoroutine(SneezyBehaviour());
    }

    private IEnumerator HappyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // don't do anything if off camera
        if (location != NightGameManager.S.GetCamLocation()) {
            // prioritize attacking if in Snow White's bedroom
            if (location == Location.snowWhiteBedroom) {
                if (NightGameManager.S.GetDoorClosed()) {
                    location = sleepyPath[0];
                    locationIndex = 0;
                    transform.position = sleepyTransformPath[0].position;
                } else {
                    // attack + player dies
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, hard chance
                if (chance < finalDifficulty) {
                    Move();
                    // physically move
                    transform.position = sleepyTransformPath[locationIndex].position;
                }
                // give knife if in kitchen or further along path
            }
        }
        StartCoroutine(HappyBehaviour());
    }

    private IEnumerator GrumpyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // don't do anything if off camera
        if (location != NightGameManager.S.GetCamLocation()) {
            // prioritize attacking if in Snow White's bedroom
            if (location == Location.snowWhiteBedroom) {
                if (NightGameManager.S.GetDoorClosed()) {
                    location = sleepyPath[0];
                    locationIndex = 0;
                    transform.position = sleepyTransformPath[0].position;
                } else {
                    // attack + player dies
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, hard chance
                if (chance < finalDifficulty) {
                    Move();
                    // physically move
                    transform.position = sleepyTransformPath[locationIndex].position;
                }
                // give knife if in kitchen or further along path
            }
        }
        StartCoroutine(GrumpyBehaviour());
    }
*/

}
