using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Dwarf{dopey, sleepy, bashful, doc, sneezy, happy, grumpy};
public enum Location{dwarfBedroom, bathroom, workshop, unknown, mineEntrance, hallOne, hallTwo, livingRoom, kitchen, snowWhiteBedroom, none};

public class NightDwarfBehaviour : MonoBehaviour
{
    [Range(0, 20)]
    public int difficultAdjustment = 0;

    public Dwarf dwarf;
    public Transform death;

    public Location[] sleepyPathHallOneLiving;
    public Location[] sleepyPathBedroomLiving;
    public Location[] sleepyPathBedroomUnknownLiving;
    public Location[] bashfulPathKitchenLiving;
    public Location[] bashfulPathKitchenUnknown;
    public Location[] bashfulPathBathroomUnknown;
    public Location[] docPathUnknownLiving;
    public Location[] docPathUnknownBathroom;
    public Location[] sneezyPathBathroomUnknown;
    public Location[] happyPathBedroomBathroom;
    public Location[] happyPathBedroomWorkshopBathroom;
    public Location[] grumpyPathWorkshopLiving;

    public Transform[] sleepyTransformPathHallOneLiving;
    public Transform[] sleepyTransformPathBedroomLiving;
    public Transform[] sleepyTransformPathBedroomUnknownLiving;
    public Transform[] bashfulTransformPathKitchenLiving;
    public Transform[] bashfulTransformPathKitchenUnknown;
    public Transform[] bashfulTransformPathBathroomUnknown;
    public Transform[] docTransformPathUnknownLiving;
    public Transform[] docTransformPathUnknownBathroom;
    public Transform[] sneezyTransformPathBathroomUnknown;
    public Transform[] happyTransformPathBedroomBathroom;
    public Transform[] happyTransformPathBedroomWorkshopBathroom;
    public Transform[] grumpyTransformPathWorkshopLiving;

    public AudioClip sneeze;
    public AudioClip mumble;
    public AudioClip angry;
    public AudioClip laugh;
    public AudioClip deathSound;
    public AudioClip doorSlam;

    private bool isActive = false;
    private bool onCamera = false;
    private bool playerDead = false;
    private bool playerEyesOpen = true;

    private bool docFree = false; // key 1, study
    private bool sneezyFree = false; // key 2, bathroom
    private bool happyFree = false; // key 3, dwarf bedroom
    private bool grumpyFree = false; // key 4, workshop

    private float moveTimer = 0f;
    private int shortWait = 0;
    private int longWait = 0;
    private int movementIndex = 0;

    private Location location;
    private Location[] locationPath;
    private Transform[] transformPath;

    private AudioSource audio;
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        StartNight();
    }

    private void StartNight() {
        anim.SetBool("attacking", false);
        movementIndex = 0;
        GetFreeDwarves();
        NightGameManager.S.GetDifficulty(dwarf);
        DwarfStart();
    }

    private void GetFreeDwarves() {
        docFree = PlayerPrefs.GetInt("DwarfFree3") == 1;
        sneezyFree = PlayerPrefs.GetInt("DwarfFree4") == 1;
        happyFree = PlayerPrefs.GetInt("DwarfFree5") == 1;
        grumpyFree = PlayerPrefs.GetInt("DwarfFree6") == 1;
    }

    private void DwarfStart() {
        DwarfSetup();
        GoHome();
        SetWaitTimes();
        if (difficultAdjustment > 0) StartCoroutine(Move());
    }

    private void DwarfSetup() {
        switch(dwarf) {
            case Dwarf.sleepy:
                SleepyStart();
                break;
            case Dwarf.bashful:
                BashfulStart();
                break;
            case Dwarf.doc:
                DocStart();
                break;
            case Dwarf.sneezy:
                SneezyStart();
                break;
            case Dwarf.happy:
                HappyStart();
                break;
            case Dwarf.grumpy:
                GrumpyStart();
                break;
            default:
                Debug.Log("something is wrong");
                break;
        }
    }

    public void SetDifficulty(int diff) {
        difficultAdjustment = Mathf.Clamp(diff, 0, 20);
    }

    private void SetWaitTimes() {
        shortWait = Mathf.Clamp(15 - difficultAdjustment, 5, 20);
        longWait = Mathf.Clamp(25 - difficultAdjustment, 5, 20);
    }

    private int GetWaitTime() {
        if (!playerEyesOpen && difficultAdjustment > 0) {
            return (int)Mathf.Log(difficultAdjustment + 1);
        }
        return (int)Random.RandomRange(shortWait, longWait);
    }

    private void GoHome() {
        location = locationPath[0];
        movementIndex = 0;
        transform.position = transformPath[0].position;
        transform.rotation = transformPath[0].rotation;
    }

    private void SleepyStart() {
        if (happyFree) {
            locationPath = sleepyPathBedroomLiving;
            transformPath = sleepyTransformPathBedroomLiving;
        } else {
            locationPath = sleepyPathHallOneLiving;
            transformPath = sleepyTransformPathHallOneLiving;
        }
    }

    private void BashfulStart() {
        if (sneezyFree) {
            locationPath = bashfulPathBathroomUnknown;
            transformPath = bashfulTransformPathBathroomUnknown;
        } else {
            locationPath = bashfulPathKitchenLiving;
            transformPath = bashfulTransformPathKitchenLiving;
        }
    }

    private void DocStart() {
        if (sneezyFree) {
            locationPath = docPathUnknownBathroom;
            transformPath = docTransformPathUnknownBathroom;
        } else {
            locationPath = docPathUnknownLiving;
            transformPath = docTransformPathUnknownLiving;
        }
    }

    private void SneezyStart() {
        locationPath = sneezyPathBathroomUnknown;
        transformPath = sneezyTransformPathBathroomUnknown;
    }

    private void HappyStart() {
        if (grumpyFree) {
            locationPath = happyPathBedroomWorkshopBathroom;
            transformPath = happyTransformPathBedroomWorkshopBathroom;
        } else {
            locationPath = happyPathBedroomBathroom;
            transformPath = happyTransformPathBedroomBathroom;
        }
    }

    private void GrumpyStart() {
        locationPath = grumpyPathWorkshopLiving;
        transformPath = grumpyTransformPathWorkshopLiving;
    }

    private IEnumerator Move() {
        yield return new WaitForSeconds(GetWaitTime());
        // roll to move
        if (difficultAdjustment > (int)Random.Range(1, 20)) {
            // prioritize attacking
            if (location == Location.snowWhiteBedroom) {
                // check special blocked cases, then the rest
                if (dwarf == Dwarf.grumpy && NightGameManager.S.GetDoorClosed()) {
                    StartCoroutine(DoSlam());
                } else if (dwarf == Dwarf.sleepy && NightGameManager.S.GetDoorClosed()) { // other blocked cases
                    GoHome();
                } else if (dwarf == Dwarf.sleepy && NightGameManager.S.GetDoorClosed()) {
                    GoHome();
                } else if (dwarf == Dwarf.bashful && NightGameManager.S.GetFireLit()) {
                    GoHome();
                } else if ((dwarf == Dwarf.doc || dwarf == Dwarf.happy) && NightGameManager.S.GetVentClosed()) {
                    GoHome();
                } else { // able to attack
                    audio.PlayOneShot(deathSound);
                    transform.position = death.position;
                    transform.rotation = death.rotation;
                    playerDead = true;
                    KillPlayer(); // start countdown to force mirror down
                }
            } else if (location != NightGameManager.S.GetCamLocation() && locationPath[movementIndex + 1] != NightGameManager.S.GetCamLocation()) { // move along path if path isn't blocked and player isn't looking at dwarf or dwarf's next room
                if ((dwarf == Dwarf.sleepy || dwarf == Dwarf.grumpy) && !NightGameManager.S.GetDoorClosed()) {
                    if (CheckDoubleCams()) {
                        if (dwarf == Dwarf.grumpy) audio.PlayOneShot(angry);
                        DoMove();
                    }
                } else if ((dwarf == Dwarf.doc || dwarf == Dwarf.happy) && !NightGameManager.S.GetVentClosed()) {
                    if (CheckDoubleCams()) {
                        if (dwarf == Dwarf.doc) audio.PlayOneShot(mumble);
                        else audio.PlayOneShot(laugh);
                        DoMove();
                    }
                } else if (dwarf == Dwarf.sneezy && NightGameManager.S.GetFireLit() && (location == Location.unknown || locationPath[movementIndex + 1] == Location.unknown)) {
                    DoSneeze();
                } else if (dwarf == Dwarf.bashful && NightGameManager.S.GetFireLit() && !(location == Location.unknown || locationPath[movementIndex + 1] == Location.unknown)) {
                    if (dwarf == Dwarf.sneezy) audio.PlayOneShot(sneeze);
                    DoMove();
                }
            }
        }
        if (!playerDead) StartCoroutine(Move());
    }

    private IEnumerator KillPlayer() {  // start countdown to force mirror down
        yield return new WaitForSeconds(6f);
        PlayerDies();
        NightGameManager.S.Die();
    }

    public void PlayerDies() { // killing animation & resetting game
        playerDead = true;
        anim.SetBool("attacking", true);
        StopAllCoroutines();
    }

    public void ResetDwarf() {
        DwarfSetup();
        movementIndex = 0;
        location = locationPath[movementIndex];
        transform.position = transformPath[movementIndex].position;
        transform.rotation = transformPath[movementIndex].rotation;
    }

    private void DoMove() {
        movementIndex++;
        location = locationPath[movementIndex];
        transform.position = transformPath[movementIndex].position;
        transform.rotation = transformPath[movementIndex].rotation;
    }

    public void PlayerLitFire() {
        if (location == Location.snowWhiteBedroom) {
            if (dwarf == Dwarf.bashful) {
                StopAllCoroutines();
                GoHome();
                StartCoroutine(Move());
            } else if (dwarf == Dwarf.sneezy) {
                StopAllCoroutines();
                DoSneeze();
            }
        }
    }

    private void DoSneeze() {
        audio.PlayOneShot(sneeze);
        NightGameManager.S.PutFireOut();
        GoHome();
        StartCoroutine(Move());
    }

    private IEnumerator DoSlam() {
        audio.PlayOneShot(doorSlam);
        yield return new WaitForSeconds(doorSlam.length);
        audio.PlayOneShot(doorSlam);
        GoHome();
        StartCoroutine(Move());
    }

    public int GetDifficulty() {
        return difficultAdjustment;
    }

    private bool CheckDoubleCams() {
        if (location == Location.hallOne && NightGameManager.S.GetCamLocation() == Location.hallTwo) return false;
        if (location == Location.hallTwo && NightGameManager.S.GetCamLocation() == Location.hallOne) return false;
        if (location == Location.livingRoom && NightGameManager.S.GetCamLocation() == Location.kitchen) return false;
        return true;
    }

    public void PlayerClosedEyes() {
        playerEyesOpen = false;
        StopCoroutine(Move());
        StartCoroutine(Move());
    }

    public void PlayerOpenedEyes() {
        playerEyesOpen = true;
        StopCoroutine(Move());
        StartCoroutine(Move());
    }
}
