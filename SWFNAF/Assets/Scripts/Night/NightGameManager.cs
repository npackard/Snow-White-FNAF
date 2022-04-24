using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NightGameManager : MonoBehaviour
{
    public static NightGameManager S;

    public int secondsPerHour = 35;
    
    public AudioSource fireAudio;
    public AudioSource fireIgnite;

    public GameObject cover;
    public GameObject door;
    public GameObject fire;

    public NightDwarfBehaviour sleepy;
    public NightDwarfBehaviour bashful;
    public NightDwarfBehaviour doc;
    public NightDwarfBehaviour sneezy;
    public NightDwarfBehaviour happy;
    public NightDwarfBehaviour grumpy;

    public TextMeshProUGUI deathText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI timerText;

    public AudioClip igniteClip;
    public AudioClip crackleClip;

    public NightSleep eyes;

    private Animator doorAnim;

    private Location camAt = Location.none;
    private Location lastCam = Location.none;

    private bool docFree = false; // key 1, study
    private bool sneezyFree = false; // key 2, bathroom
    private bool happyFree = false; // key 3, dwarf bedroom
    private bool grumpyFree = false; // key 4, workshop

    private bool alive = true;
    private bool doorClosed = false;
    private bool eyesClosed = false;
    private bool fireLit = false;
    private bool ventClosed = false;
    private float energy = 0f;
    public int timePassed = 0;

    private int easy = 3;
    private int medium = 5;
    private int hard = 7;

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
        PlayerPrefs.SetInt("nights", PlayerPrefs.GetInt("night") + 1);
        doorAnim = door.GetComponent<Animator>();
        GetFreeDwarves();
        fire.SetActive(false);
        timerText.text = "12am";
        deathText.enabled = false;
        energyText.text = "Energy: 0";
        lastCam = Location.dwarfBedroom;
        StartCoroutine(MakeTimePass());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetFreeDwarves() {
        docFree = PlayerPrefs.GetInt("DwarfFree3") == 1;
        sneezyFree = PlayerPrefs.GetInt("DwarfFree4") == 1;
        happyFree = PlayerPrefs.GetInt("DwarfFree5") == 1;
        grumpyFree = PlayerPrefs.GetInt("DwarfFree6") == 1;
    }

    public int GetDifficulty(Dwarf dwarf) {
        int value = 3;
        if (docFree) value += 2;
        if (sneezyFree) value += 2;
        if (happyFree) value += 2;
        if (grumpyFree) {
            value += 2;
            value += Mathf.Clamp((PlayerPrefs.GetInt("night") - 3), 0, 20);
        }
        return value;
    }

    // indicates which night is next
    private void NightEnd() {
        deathText.text = "6 am";
        deathText.enabled = true;
        ResetDwarves();
        timePassed = 0;
        StartCoroutine(MoveToDay());
    }

    private void ResetDwarves() {
        sleepy.ResetDwarf();
        bashful.ResetDwarf();
        doc.ResetDwarf();
        sneezy.ResetDwarf();
        happy.ResetDwarf();
        grumpy.ResetDwarf();
    }

    private IEnumerator MakeTimePass() {
        if (timePassed < secondsPerHour * 6f) {
            yield return new WaitForSeconds(1f);
            timePassed++;
            if (timePassed > secondsPerHour * 5f) timerText.text = "5am";
            else if (timePassed > secondsPerHour * 4f) timerText.text = "4am";
            else if (timePassed > secondsPerHour * 3f) timerText.text = "3am";
            else if (timePassed > secondsPerHour * 2f) timerText.text = "2am";
            else if (timePassed > secondsPerHour) timerText.text = "1am";
            else timerText.text = "12am";
            StartCoroutine(MakeTimePass());
        } else {
            timerText.text = "6am";
            NightEnd();
        }
    }

    private void StartNight() {
        StartCoroutine(MakeTimePass());
    }

    public void SwitchToDwarfBedroom() {
        lastCam = camAt;
        camAt = Location.dwarfBedroom;
    }

    public void SwitchToBathroom() {
        lastCam = camAt;
        camAt = Location.bathroom;
    }
    
    public void SwitchToWorkshop() {
        lastCam = camAt;
        camAt = Location.workshop;
    }

    public void SwitchToUnknown() {
        lastCam = camAt;
        camAt = Location.unknown;
    }

    public void SwitchToMineEntrance() {
        lastCam = camAt;
        camAt = Location.mineEntrance;
    }

    public void SwitchToHallOne() {
        lastCam = camAt;
        camAt = Location.hallOne;
    }

    public void SwitchToHallTwo() {
        lastCam = camAt;
        camAt = Location.hallTwo;
    }

    public void SwitchToLivingRoom() {
        lastCam = camAt;
        camAt = Location.livingRoom;
    }

    public void SwitchToKitchen() {
        lastCam = camAt;
        camAt = Location.kitchen;
    }

    public void MirrorUp() {
        camAt = lastCam;
    }

    public void MirrorDown() {
        lastCam = camAt;
        camAt = Location.none;
    }

    public Location GetCamLocation() {
        return camAt;
    }

    public bool GetDoorClosed() {
        return doorClosed;
    }

    public bool GetFireLit() {
        return fireLit;
    }

    public bool GetVentClosed() {
        return ventClosed;
    }

    public void AddEnergy(float amount) {
        if (energy < 100) energy += amount;
        energyText.text = "Energy: " + energy.ToString();
    }

    public void SwitchDoor() {
        // open vent
        ventClosed = false;
        cover.SetActive(false);
        // close door
        doorClosed = true;
        doorAnim.SetBool("open", false);
        // put out fire
        PutFireOut();
    }

    public void LightFire() {
        // open vent
        ventClosed = false;
        cover.SetActive(false);
        // open door
        doorClosed = false;
        doorAnim.SetBool("open", true);
        // light fire
        fire.SetActive(true);
        fireIgnite.PlayOneShot(igniteClip);
        StartCoroutine(StartCrackle());
        StartCoroutine(FireTimer());
        fireLit = true;
        // kick dwarves out of fireplace
        bashful.PlayerLitFire();
        sneezy.PlayerLitFire();
    }

    private IEnumerator FireTimer() {
        yield return new WaitForSeconds(5f);
        fireAudio.loop = false;
        fireAudio.Stop();
        fireLit = false;
        fire.SetActive(false);
    }

    public void SwitchVent() {
        // close vent
        ventClosed = true;
        cover.SetActive(true);
        // open door
        doorClosed = false;
        doorAnim.SetBool("open", true);
        // put out fire
        PutFireOut();
    }

    public void PutFireOut() {
        StopCoroutine(FireTimer());
        fire.SetActive(false);
        fireAudio.loop = false;
        fireAudio.Stop();
        fireLit = false;
    }

    public void Die() {
        alive = false;
        deathText.text = "You Died";
        deathText.enabled = true;
    }

    public void StartLevelAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator MoveToDay() {
        PlayerPrefs.SetFloat("Energy", energy);
        yield return new WaitForSeconds(4f);
        GameManager.instance.EndNight();
    }

    public void CloseEyes() {
        eyesClosed = true;
    }

    public void OpenEyes() {
        eyesClosed = false;
    }

    public bool GetEyesClosed() {
        return eyesClosed;
    }

    private IEnumerator StartCrackle() {
        yield return new WaitForSeconds(igniteClip.length);
        fireAudio.loop = true;
        fireAudio.Play();
    }
}
