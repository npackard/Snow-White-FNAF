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
    private float moveTimer = 0f;
    private int shortWait = 25;
    private int longWait = 50;
    private int locationIndex = 0;

    private Location location;
    
    // Start is called before the first frame update
    void Start()
    {
        location = Location.dwarfBedroom;
        transform.position = sleepyTransformPath[0].position;
        SetTimes();
        introNight = -1;
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
        float time = GetWaitTime();
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        // don't do anything if on camera
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
                // move to next room in path, easy chance
                if (chance < (difficultAdjustment / 100f) * easyChance) {
                    locationIndex++;
                    location = sleepyPath[locationIndex];
                    // physically move
                    transform.position = sleepyTransformPath[locationIndex].position;
                }
                // give knife if in kitchen or further along path
            }
        }
        StartCoroutine(SleepyBehaviour());
    }

    // stays off camera as much as possible, arrives via fireplace
    private IEnumerator BashfulBehaviour() {
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
                // move to next room in path, easy chance
                if (chance < (difficultAdjustment / 100f) * easyChance) {
                    locationIndex++;
                    location = sleepyPath[locationIndex];
                    // physically move
                    transform.position = sleepyTransformPath[locationIndex].position;
                }
                // give knife if in kitchen or further along path
            }
        }
    }

    private IEnumerator DocBehaviour() {
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
                // move to next room in path, easy chance
                if (chance < (difficultAdjustment / 100f) * easyChance) {
                    locationIndex++;
                    location = sleepyPath[locationIndex];
                    // physically move
                    transform.position = sleepyTransformPath[locationIndex].position;
                }
                // give knife if in kitchen or further along path
            }
        }
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
                    transform.position = sleepyTransformPath[0].position;
                } else {
                    // attack + player dies
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            } else {
                float chance = Random.Range(0f, 1f);
                // move to next room in path, easy chance
                if (chance < (difficultAdjustment / 100f) * easyChance) {
                    locationIndex++;
                    location = sleepyPath[locationIndex];
                    // physically move
                    transform.position = sleepyTransformPath[locationIndex].position;
                }
                // give knife if in kitchen or further along path
            }
        }
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
                // move to next room in path, easy chance
                if (chance < (difficultAdjustment / 100f) * easyChance) {
                    locationIndex++;
                    location = sleepyPath[locationIndex];
                    // physically move
                    transform.position = sleepyTransformPath[locationIndex].position;
                }
                // give knife if in kitchen or further along path
            }
        }
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
                // move to next room in path, easy chance
                if (chance < (difficultAdjustment / 100f) * easyChance) {
                    locationIndex++;
                    location = sleepyPath[locationIndex];
                    // physically move
                    transform.position = sleepyTransformPath[locationIndex].position;
                }
                // give knife if in kitchen or further along path
            }
        }
    }
}
