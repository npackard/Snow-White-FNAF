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

    private bool isActive = false;
    private bool onCamera = false;
    private bool playerDead = false;

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
        GoHome();
        SetWaitTimes();
        if (difficultAdjustment > 0) StartCoroutine(Move());
    }

    public void SetDifficulty(int diff) {
        difficultAdjustment = Mathf.Clamp(diff, 0, 20);
    }

    private void SetWaitTimes() {
        shortWait = Mathf.Clamp(15 - difficultAdjustment, 5, 20);
        longWait = Mathf.Clamp(25 - difficultAdjustment, 5, 20);
    }

    private int GetWaitTime() {
        return (int)Random.Range(shortWait, longWait);
    }

    private void GoHome() {
        location = locationPath[0];
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
                    // bang on door
                    GoHome();
                } else if (dwarf == Dwarf.sleepy && NightGameManager.S.GetDoorClosed()) { // other blocked cases
                    GoHome();
                } else if (dwarf == Dwarf.sleepy && NightGameManager.S.GetDoorClosed()) {
                    GoHome();
                } else if (dwarf == Dwarf.bashful && NightGameManager.S.GetFireLit()) {
                    GoHome();
                } else if ((dwarf == Dwarf.doc || dwarf == Dwarf.happy) && NightGameManager.S.GetVentClosed()) {
                    GoHome();
                } else { // able to attack
                    transform.position = death.position;
                    transform.rotation = death.rotation;
                    playerDead = true;
                    KillPlayer(); // start countdown to force mirror down
                }
            } else { // move along path if path isn't blocked
                if ((dwarf == Dwarf.sleepy || dwarf == Dwarf.grumpy) && !NightGameManager.S.GetDoorClosed()) {
                    if (dwarf == Dwarf.grumpy) audio.PlayOneShot(angry);
                    DoMove();
                } else if ((dwarf == Dwarf.doc || dwarf == Dwarf.happy) && !NightGameManager.S.GetVentClosed()) {
                    if (dwarf == Dwarf.doc) audio.PlayOneShot(mumble);
                    else audio.PlayOneShot(laugh);
                    DoMove();
                } else if ((dwarf == Dwarf.bashful || dwarf == Dwarf.sneezy) && !NightGameManager.S.GetFireLit()) {
                    DoMove();
                } else if (dwarf == Dwarf.sneezy && NightGameManager.S.GetFireLit()) {
                    DoSneeze();
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
        anim.SetBool("attacking", true);
        StopAllCoroutines();
    }

    public void ResetDwarf() {
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
        if (dwarf == Dwarf.bashful) {
            StopAllCoroutines();
            GoHome();
            StartCoroutine(Move());
        } else if (dwarf == Dwarf.sneezy) {
            StopAllCoroutines();
            DoSneeze();
        }
    }

    private void DoSneeze() {
        audio.PlayOneShot(sneeze);
        NightGameManager.S.PutFireOut();
        GoHome();
        StartCoroutine(Move());
    }





    /*
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

    public AudioClip sneeze;
    public AudioClip mumble;
    public AudioClip angry;
    public AudioClip laugh;

    private bool isActive = false;
    private bool isEnabled = false;
    private bool onCamera = false;
    private bool dwarfThreeFree;
    private bool dwarfFourFree;
    private bool dwarfFiveFree;
    private bool dwarfSixFree;
    private bool key1;
    private bool key2;
    private bool key3;
    private bool key4;
    private float finalDifficulty = 1f;
    private float moveTimer = 0f;
    private int shortWait = 25;
    private int longWait = 50;
    private int locationIndex = 0;

    private Location location;
    private List<Location> locBefore;

    private List<Location> locationPath;
    private List<Transform> transformPath;
    private List<Location> all;

    private AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        locationPath = new List<Location>();
        transformPath = new List<Transform>();
        all = new List<Location>{Location.dwarfBedroom, Location.bathroom, Location.workshop, Location.unknown, Location.hallOne, Location.hallTwo, Location.livingRoom, Location.kitchen};
        audio = GetComponent<AudioSource>();
        GetKeys();
        StartingPos();
        SetTimes();
        MakePath(dwarf);
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
            case Dwarf.sleepy: // living room
                transform.position = sleepyTransformPath[0].position;
                transform.rotation = sleepyTransformPath[0].rotation;
                if (key4) location = Location.dwarfBedroom;
                else location = Location.livingRoom;
                locBefore = NightPathfinder.S.SleepyPath();
                break;
            case Dwarf.bashful: // unknown
                transform.position = bashfulTransformPath[0].position;
                transform.rotation = bashfulTransformPath[0].rotation;
                if (key3) location = Location.bathroom;
                else location = Location.kitchen;
                locBefore = NightPathfinder.S.BasfhulPath();
                break;
            case Dwarf.doc: // bathroom
                transform.position = docTransformPath[0].position;
                transform.rotation = docTransformPath[0].rotation;
                location = Location.bathroom;
                locBefore = NightPathfinder.S.DocPath();
                break;
            case Dwarf.sneezy: // unknown
                transform.position = sneezyTransformPath[0].position;
                transform.rotation = sneezyTransformPath[0].rotation;
                location = Location.dwarfBedroom;
                locBefore = NightPathfinder.S.SneezyPath();
                break;
            case Dwarf.happy: // bathroom
                transform.position = happyTransformPath[0].position;
                transform.rotation = happyTransformPath[0].rotation;
                location = Location.unknown;
                locBefore = NightPathfinder.S.HappyPath();
                break;
            case Dwarf.grumpy: // living room
                transform.position = grumpyTransformPath[0].position;
                transform.rotation = grumpyTransformPath[0].rotation;
                location = Location.workshop;
                locBefore = NightPathfinder.S.GrumpyPath();
                break;
            default:
                Debug.Log("oh no");
                break;
        }
    }

    private void GetKeys() {
        key1 = PlayerPrefs.GetInt("Key1") == 1;
        key2 = PlayerPrefs.GetInt("Key2") == 1;
        key3 = PlayerPrefs.GetInt("Key3") == 1;
        key4 = PlayerPrefs.GetInt("Key4") == 1;
    }

    private void GetDwarves() {
        dwarfThreeFree = PlayerPrefs.GetInt("DwarfFree3") == 1;
        dwarfFourFree = PlayerPrefs.GetInt("DwarfFree4") == 1;
        dwarfFiveFree = PlayerPrefs.GetInt("DwarfFree5") == 1;
        dwarfSixFree = PlayerPrefs.GetInt("DwarfFree6") == 1;
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
        } else if (dwarf == Dwarf.happy || dwarf == Dwarf.grumpy) {
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
        locationIndex = 0;
        StartCoroutine(MoveDwarf());
    }

    private int GetWaitTime() {
        return (int)Random.Range(shortWait, longWait);
    }

    private void MakePath(Dwarf dwarf) {
        int index = -1;
        switch(dwarf) {
            case Dwarf.sleepy:
                index = 6;
                break;
            case Dwarf.bashful:
                index = 3;
                break;
            case Dwarf.doc:
                index = 1;
                break;
            case Dwarf.sneezy:
                index = 3;
                break;
            case Dwarf.happy:
                index = 1;
                break;
            case Dwarf.grumpy:
                index = 6;
                break;
            default:
                Debug.Log("that's not good");
                break;
        }

        int ind = -1;
        Location curLocation = location;
        locationPath.Add(curLocation);
        for (int i = 0; i < all.Count; i++) {
                if (all[i] == curLocation) ind = i;
            }
            // go to next location
            curLocation = locBefore[index];
        while (curLocation != location && curLocation != Location.none) {
            locationPath.Add(curLocation);
            // find current index
            for (int i = 0; i < all.Count; i++) {
                if (all[i] == curLocation) ind = i;
            }
            // go to next location
            curLocation = locBefore[index];
        }
        locationPath.Reverse();

        for (int i = 0; i < locationPath.Count; i++) {
            foreach(Location loc in all) {
                if (loc == locationPath[i]) {
                    switch(dwarf) {
                        case Dwarf.sleepy:
                            transformPath.Add(sleepyTransformPath[i]);
                            break;
                        case Dwarf.bashful:
                            transformPath.Add(bashfulTransformPath[i]);
                            break;
                        case Dwarf.doc:
                            transformPath.Add(docTransformPath[i]);
                            break;
                        case Dwarf.sneezy:
                            transformPath.Add(sneezyTransformPath[i]);
                            break;
                        case Dwarf.happy:
                            transformPath.Add(happyTransformPath[i]);
                            break;
                        case Dwarf.grumpy:
                            transformPath.Add(grumpyTransformPath[i]);
                            break;
                        default:
                            Debug.Log("something is wrong");
                            break;
                    }
                }
            }
        }

        switch(dwarf) {
            case Dwarf.sleepy:
                transformPath.Add(sleepyTransformPath[sleepyTransformPath.Length - 1]);
                break;
            case Dwarf.bashful:
                transformPath.Add(bashfulTransformPath[bashfulTransformPath.Length - 1]);
                break;
            case Dwarf.doc:
                transformPath.Add(docTransformPath[docTransformPath.Length - 1]);
                break;
            case Dwarf.sneezy:
                transformPath.Add(sneezyTransformPath[sneezyTransformPath.Length - 1]);
                break;
            case Dwarf.happy:
                transformPath.Add(happyTransformPath[happyTransformPath.Length - 1]);
                break;
            case Dwarf.grumpy:
                transformPath.Add(grumpyTransformPath[grumpyTransformPath.Length - 1]);
                break;
            default:
                Debug.Log("oh no");
                break;
        }

        Debug.Log(transformPath.Count);
        locationPath.Add(Location.snowWhiteBedroom);
    }

    private IEnumerator MoveDwarf() {
        yield return new WaitForSeconds(GetWaitTime());
        if (location == Location.snowWhiteBedroom && NightGameManager.S.GetCamLocation() == Location.none) {
            if ((dwarf == Dwarf.sleepy || dwarf == Dwarf.grumpy) && NightGameManager.S.GetDoorClosed()) {
                locationIndex = 0;
                location = locationPath[0];
                transform.position = transformPath[0].position;
                transform.rotation = transformPath[0].rotation;
            } else if ((dwarf == Dwarf.bashful || dwarf == Dwarf.sneezy) && NightGameManager.S.GetFireLit()) {
                locationIndex = 0;
                location = locationPath[0];
                transform.position = transformPath[0].position;
                transform.rotation = transformPath[0].rotation;
            } else if ((dwarf == Dwarf.doc || dwarf == Dwarf.happy) && NightGameManager.S.GetVentClosed()) {
                locationIndex = 0;
                location = locationPath[0];
                transform.position = transformPath[0].position;
                transform.rotation = transformPath[0].rotation;
            } else {
                if (NightGameManager.S.GetEyesClosed()) NightGameManager.S.eyes.NoControl();
                transform.position = deathPosition.position;
                transform.rotation = deathPosition.rotation;
                StartCoroutine(Die());
            }
        } else if (location == Location.snowWhiteBedroom) {
                // cam is up
                locationIndex = 0;
                location = locationPath[0];
                transform.position = transformPath[0].position;
                transform.rotation = transformPath[0].rotation;
        } else if (location != NightGameManager.S.GetCamLocation() && locationPath[locationIndex + 1] != NightGameManager.S.GetCamLocation()) {
            float chance = Random.Range(0f, 1f);
            Debug.Log(chance);
            if (chance < finalDifficulty) {
                if (dwarf == Dwarf.doc) audio.PlayOneShot(mumble);
                else if (dwarf == Dwarf.sneezy) audio.PlayOneShot(sneeze);
                else if (dwarf == Dwarf.happy) audio.PlayOneShot(laugh);
                else if (dwarf == Dwarf.grumpy) audio.PlayOneShot(angry);
                locationIndex++;
                location = locationPath[locationIndex];
                transform.position = transformPath[locationIndex].position;
                transform.rotation = transformPath[locationIndex].rotation;
            }
        }
        StartCoroutine(MoveDwarf());
    }

    public void ActiveFire() {
        if (dwarf == Dwarf.bashful || dwarf == Dwarf.sneezy) {
            StopAllCoroutines();
            locationIndex = 0;
            location = locationPath[0];
            transform.position = transformPath[0].position;
            transform.rotation = transformPath[0].rotation;
            StartCoroutine(MoveDwarf());
        }
    }
    */
}
