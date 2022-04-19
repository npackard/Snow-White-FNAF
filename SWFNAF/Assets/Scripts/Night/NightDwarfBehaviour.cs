using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Dwarf{dopey, sleepy, bashful, doc, sneezy, happy, grunmpy};
public enum Location{dwarfBedroom, bathroom, workshop, unknown, mineEntrance, hallOne, hallTwo, livingRoom, kitchen, snowWhiteBedroom, none};

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
        StartingPos();
        SetTimes();
        SetFinalDifficulty();
        StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartingPos() {
        location = Location.dwarfBedroom;
        switch(dwarf) {
            case Dwarf.sleepy:
                transform.position = sleepyTransformPath[0].position;
                transform.rotation = sleepyTransformPath[0].rotation;
                location = Location.dwarfBedroom;
                break;
            case Dwarf.bashful:
                transform.position = bashfulTransformPath[0].position;
                transform.rotation = bashfulTransformPath[0].rotation;
                location = Location.dwarfBedroom;
                break;
            case Dwarf.doc:
                transform.position = docTransformPath[0].position;
                transform.rotation = docTransformPath[0].rotation;
                location = Location.dwarfBedroom;
                break;
            case Dwarf.sneezy:
                transform.position = sneezyTransformPath[0].position;
                transform.rotation = sneezyTransformPath[0].rotation;
                location = Location.dwarfBedroom;
                break;
            case Dwarf.happy:
                transform.position = happyTransformPath[0].position;
                transform.rotation = happyTransformPath[0].rotation;
                location = Location.dwarfBedroom;
                break;
            case Dwarf.grunmpy:
                transform.position = grumpyTransformPath[0].position;
                transform.rotation = grumpyTransformPath[0].rotation;
                location = Location.dwarfBedroom;
                break;
            default:
                Debug.Log("oh no");
                break;
        }
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
                    StartCoroutine(DocBehaviour());
                    break;
                case Dwarf.sneezy:
                    StartCoroutine(SneezyBehaviour());
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
        yield return new WaitForSeconds(GetWaitTime());
        // prioritize attacking if in Snow White's bedroom
        if (location == Location.snowWhiteBedroom && NightGameManager.S.GetCamLocation() == Location.none) {
            if (NightGameManager.S.GetDoorClosed()) {
                location = Location.dwarfBedroom;
                locationIndex = 0;
                transform.position = sleepyTransformPath[0].position;
                transform.rotation = sleepyTransformPath[0].rotation;
            } else {
                if (NightGameManager.S.GetEyesClosed()) {
                    NightGameManager.S.eyes.NoControl();
                }
                transform.position = deathPosition.position;
                transform.rotation = deathPosition.rotation;
                StartCoroutine(Die());
            }
        } else if (location == Location.snowWhiteBedroom) {
            location = sleepyPath[0];
            locationIndex = 0;
            transform.position = sleepyTransformPath[0].position;
            transform.rotation = sleepyTransformPath[0].rotation;
        } else if (location != NightGameManager.S.GetCamLocation() && sleepyPath[locationIndex + 1] != NightGameManager.S.GetCamLocation()) {
            float chance = Random.Range(0f, 1f);
            // move to next room in path, easy chance
            if (chance < finalDifficulty) {
                locationIndex++;
                location = sleepyPath[locationIndex];
                transform.position = sleepyTransformPath[locationIndex].position;
                transform.rotation = sleepyTransformPath[locationIndex].rotation;
            }
        }
        StartCoroutine(SleepyBehaviour());
    }

    // stays off camera as much as possible, arrives via fireplace
    private IEnumerator BashfulBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // prioritize attacking if in Snow White's bedroom
        if (location == Location.snowWhiteBedroom && NightGameManager.S.GetCamLocation() == Location.none) {
            if (NightGameManager.S.GetFireLit()) {
                location = Location.dwarfBedroom;
                locationIndex = 0;
                transform.position = bashfulTransformPath[0].position;
                transform.rotation = bashfulTransformPath[0].rotation;
            } else {
                transform.position = deathPosition.position;
                transform.rotation = deathPosition.rotation;
                StartCoroutine(Die());
            }
        } else if (location == Location.snowWhiteBedroom) {
            location = bashfulPath[0];
            locationIndex = 0;
            transform.position = bashfulTransformPath[0].position;
            transform.rotation = bashfulTransformPath[0].rotation;
        } else if (location != NightGameManager.S.GetCamLocation() && bashfulPath[locationIndex + 1] != NightGameManager.S.GetCamLocation()) {
            float chance = Random.Range(0f, 1f);
            // move to next room in path, easy chance
            if (chance < finalDifficulty) {
                locationIndex++;
                location = bashfulPath[locationIndex];
                transform.position = bashfulTransformPath[locationIndex].position;
                transform.rotation = bashfulTransformPath[locationIndex].rotation;
            }
        }
        StartCoroutine(BashfulBehaviour());
    }

    private IEnumerator DocBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // prioritize attacking if in Snow White's bedroom
        if (location == Location.snowWhiteBedroom && NightGameManager.S.GetCamLocation() == Location.none) {
            // if blocked
            if (NightGameManager.S.GetVentClosed()) {
                location = Location.dwarfBedroom;
                transform.position = docTransformPath[0].position;
                transform.rotation = docTransformPath[0].rotation;
            } else {
                transform.position = deathPosition.position;
                transform.rotation = deathPosition.rotation;
                StartCoroutine(Die());
            }
        } else if (location == Location.snowWhiteBedroom) {
            location = Location.dwarfBedroom;
            locationIndex = 0;
            transform.position = docTransformPath[0].position;
            transform.rotation = docTransformPath[0].rotation;
        } else if (location != NightGameManager.S.GetCamLocation()) {
            float chance = Random.Range(0f, 1f);
            // move to next room in path, medium chance
            if (chance < finalDifficulty) {
                float newChance = Random.Range(0f, 1f);
                // potentially add difficulty check to see if movement switches locations or doesn't occur
                switch(location) {
                    case Location.dwarfBedroom:
                        if (NightGameManager.S.GetCamLocation() != Location.hallOne) {
                            location = Location.hallOne;
                            transform.position = docTransformPath[1].position;
                            transform.rotation = docTransformPath[1].rotation;
                        }
                        break;
                    case Location.hallOne:
                        if (newChance < .5 && NightGameManager.S.GetCamLocation() != Location.workshop) {
                            location = Location.workshop;
                            transform.position = docTransformPath[6].position;
                            transform.rotation = docTransformPath[6].rotation;
                        } else if (NightGameManager.S.GetCamLocation() != Location.unknown) {
                            location = Location.unknown;
                            transform.position = docTransformPath[2].position;
                            transform.rotation = docTransformPath[2].rotation;
                        }
                        break;
                    case Location.unknown:
                        if (newChance < .5 && NightGameManager.S.GetCamLocation() != Location.workshop) {
                            location = Location.workshop;
                            transform.position = docTransformPath[6].position;
                            transform.rotation = docTransformPath[6].rotation;
                        } else if (NightGameManager.S.GetCamLocation() != Location.mineEntrance) {
                            location = Location.mineEntrance;
                            transform.position = docTransformPath[3].position;
                            transform.rotation = docTransformPath[3].rotation;
                        }
                        break;
                    case Location.mineEntrance:
                        if (newChance < .5 && NightGameManager.S.GetCamLocation() != Location.kitchen) {
                            location = Location.kitchen;
                            transform.position = docTransformPath[4].position;
                            transform.rotation = docTransformPath[4].rotation;
                        } else if (NightGameManager.S.GetCamLocation() != Location.livingRoom) {
                            location = Location.livingRoom;
                            transform.position = docTransformPath[5].position;
                            transform.rotation = docTransformPath[5].rotation;
                        }
                        break;
                    case Location.kitchen:
                        if (NightGameManager.S.GetCamLocation() != Location.livingRoom) {
                            location = Location.livingRoom;
                            transform.position = docTransformPath[4].position;
                            transform.rotation = docTransformPath[4].rotation;
                        }
                        break;
                    case Location.livingRoom:
                        if (NightGameManager.S.GetCamLocation() != Location.workshop) {
                            location = Location.workshop;
                            transform.position = docTransformPath[6].position;
                            transform.rotation = docTransformPath[6].rotation;
                        }
                        break;
                    case Location.workshop:
                        if (newChance < .5 && NightGameManager.S.GetCamLocation() != Location.bathroom) {
                            location = Location.bathroom;
                            transform.position = docTransformPath[7].position;
                            transform.rotation = docTransformPath[7].rotation;
                        } else if (NightGameManager.S.GetCamLocation() != Location.livingRoom) {
                            location = Location.livingRoom;
                            transform.position = docTransformPath[5].position;
                            transform.rotation = docTransformPath[5].rotation;
                        }
                        break;
                    case Location.bathroom:
                        if (NightGameManager.S.GetCamLocation() != Location.none) {
                            location = Location.snowWhiteBedroom;
                            transform.position = docTransformPath[8].position;
                            transform.rotation = docTransformPath[8].rotation;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        StartCoroutine(DocBehaviour());
    }

    private IEnumerator SneezyBehaviour() {
        yield return new WaitForSeconds(GetWaitTime());
        // prioritize attacking if in Snow White's bedroom
        if (location == Location.snowWhiteBedroom && NightGameManager.S.GetCamLocation() == Location.none) {
            if (NightGameManager.S.GetFireLit()) {
                location = Location.dwarfBedroom;
                transform.position = bashfulTransformPath[0].position;
                transform.rotation = bashfulTransformPath[0].rotation;
            } else {
                transform.position = deathPosition.position;
                transform.rotation = deathPosition.rotation;
                StartCoroutine(Die());
            }
        } else if (location == Location.snowWhiteBedroom) {
            location = Location.dwarfBedroom;
            locationIndex = 0;
            transform.position = docTransformPath[0].position;
            transform.rotation = docTransformPath[0].rotation;
        } else if (location != NightGameManager.S.GetCamLocation()) {
            float chance = Random.Range(0f, 1f);
            // move to next room in path, medium chance
            if (chance < finalDifficulty) {
                // potentially add difficulty check to see if movement switches locations or doesn't occur
            }
        }
        StartCoroutine(SneezyBehaviour());
    }

//// NICOLE NEEDS TO CHANGE ALL OF THE FOLLOWING CODE TO MATCH SLEEPY AND BASHFUL BEHAVIOUR \\\\\

/*
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
