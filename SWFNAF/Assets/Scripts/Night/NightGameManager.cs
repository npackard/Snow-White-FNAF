using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class NightGameManager : MonoBehaviour
{
    public static NightGameManager S;

    public int secondsPerHour = 35;

    public AudioClip win;
    public AudioClip fireOut;
    
    public AudioSource fireAudio;
    public AudioSource fireIgnite;

    public GameObject cover;
    public GameObject door;
    public GameObject fire;
    public GameObject playAgain;
    public NightDopey dopey;

    public Image gameOverImage;
    public GameObject doorButton;
    public GameObject fireplaceButton;
    public GameObject ventButton;

    public NightDwarfBehaviour sleepy;
    public NightDwarfBehaviour bashful;
    public NightDwarfBehaviour doc;
    public NightDwarfBehaviour sneezy;
    public NightDwarfBehaviour happy;
    public NightDwarfBehaviour grumpy;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI timerText;

    public AudioClip igniteClip;
    public AudioClip crackleClip;

    public NightSleep eyes;

    private Animator doorAnim;
    private AudioSource audio;

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
    private int timePassed = 0;

    private int easy = 3;
    private int medium = 5;
    private int hard = 7;
    private int count = 0;

    private int lastButtonPressed = -1;

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
        audio = GetComponent<AudioSource>();
        doorAnim = door.GetComponent<Animator>();
        DoSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoSetup() {
        count = 0;
        timePassed = 0;
        lastCam = Location.dwarfBedroom;
        energy = PlayerPrefs.GetInt("Energy");
        timerText.text = "12am";
        energyText.text = "Energy: " +  energy.ToString();
        winText.enabled = false;
        energyText.enabled = true;
        timerText.enabled = true;
        playAgain.SetActive(false);
        fire.SetActive(false);
        eyes.YesControl();
        GetFreeDwarves();
        doorButton.SetActive(true);
        fireplaceButton.SetActive(docFree);
        ventButton.SetActive(sneezyFree);
        ResetDwarves();
        dopey.Home();
        StartCoroutine(MakeTimePass());
    }

    private void GetFreeDwarves() {
        docFree = PlayerPrefs.GetInt("DwarfFree3") == 1;
        sneezyFree = PlayerPrefs.GetInt("DwarfFree4") == 1;
        happyFree = PlayerPrefs.GetInt("DwarfFree5") == 1;
        grumpyFree = PlayerPrefs.GetInt("DwarfFree6") == 1;
    }

    public int GetDifficulty(Dwarf dwarf) {
        int value = 3;
        if (docFree) value += 1;
        if (sneezyFree) value += 1;
        if (happyFree) value += 1;
        if (grumpyFree) {
            value += 1;
            value += Mathf.Clamp((PlayerPrefs.GetInt("night") - 3), 0, 20);
        }
        return value;
    }

    // indicates which night is next
    private void NightEnd() {
        winText.text = "6 am";
        winText.enabled = true;
        ResetDwarves();
        timePassed = 0;
        StartCoroutine(MoveToDay());
    }

    private void ResetDwarves() {
        doc.gameObject.SetActive(true);
        sneezy.gameObject.SetActive(true);
        happy.gameObject.SetActive(true);
        grumpy.gameObject.SetActive(true);
        sleepy.ResetDwarf();
        bashful.ResetDwarf();
        doc.ResetDwarf();
        sneezy.ResetDwarf();
        happy.ResetDwarf();
        grumpy.ResetDwarf();
        if (!grumpyFree) grumpy.gameObject.SetActive(false);
        if (!happyFree) happy.gameObject.SetActive(false);
        if (!sneezyFree) sneezy.gameObject.SetActive(false);
        if (!docFree) doc.gameObject.SetActive(false);
    }

    private IEnumerator MakeTimePass() {
        if (timePassed < secondsPerHour * 6f) {
            yield return new WaitForSeconds(1f);
            timePassed++;
            if (eyesClosed) timePassed++;
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
        Debug.Log("please");
        dopey.Door();
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
        dopey.Fire();
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
        if (bashful.gameObject.activeInHierarchy) bashful.PlayerLitFire();
        if (sneezy.gameObject.activeInHierarchy) sneezy.PlayerLitFire();
    }

    private IEnumerator FireTimer() {
        yield return new WaitForSeconds(5f);
        fireAudio.loop = false;
        fireAudio.Stop();
        audio.PlayOneShot(fireOut);
        fireLit = false;
        fire.SetActive(false);
        dopey.Home();
    }

    public void SwitchVent() {
        dopey.Vent();
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
        if (dopey.AtFire()) dopey.Home();
        StopCoroutine(FireTimer());
        fire.SetActive(false);
        fireLit = false;
    }

    public void Die() {
        StopAllCoroutines();
        eyes.NoControl();
        OpenEyes();
        NightMoveMirror.S.MirrorDown();
        alive = false;
        sleepy.PlayerDies();
        bashful.PlayerDies();
        doc.PlayerDies();
        sneezy.PlayerDies();
        happy.PlayerDies();
        grumpy.PlayerDies();
        winText.enabled = false;
        energyText.enabled = false;
        timerText.enabled = false;
        gameOverImage.enabled = true;
        StartCoroutine(GameOver());
    }

    public void StartLevelAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator MoveToDay() {
        PlayerPrefs.SetFloat("Energy", energy);
        audio.PlayOneShot(win);
        yield return new WaitForSeconds(win.length);
        GameManager.instance.EndNight();
    }

    public void CloseEyes() {
        eyesClosed = true;
        sleepy.PlayerClosedEyes();
        bashful.PlayerClosedEyes();
        if (doc.gameObject.activeInHierarchy) doc.PlayerClosedEyes();
        if (sneezy.gameObject.activeInHierarchy) sneezy.PlayerClosedEyes();
        if (happy.gameObject.activeInHierarchy) happy.PlayerClosedEyes();
        if (grumpy.gameObject.activeInHierarchy) grumpy.PlayerClosedEyes();
        NightSleep.S.CloseEyes();
    }

    public void OpenEyes() {
        eyesClosed = false;
        sleepy.PlayerOpenedEyes();
        bashful.PlayerOpenedEyes();
        if (doc.gameObject.activeInHierarchy) doc.PlayerOpenedEyes();
        if (sneezy.gameObject.activeInHierarchy) sneezy.PlayerOpenedEyes();
        if (happy.gameObject.activeInHierarchy) happy.PlayerOpenedEyes();
        if (grumpy.gameObject.activeInHierarchy) grumpy.PlayerOpenedEyes();
        NightSleep.S.OpenEyes();
    }

    public bool GetEyesClosed() {
        return eyesClosed;
    }

    private IEnumerator StartCrackle() {
        yield return new WaitForSeconds(igniteClip.length);
        if (fireLit) {
            fireAudio.loop = true;
            fireAudio.Play();
        }
    }

    public int GetTotalDifficulty() {
        int value = sleepy.GetDifficulty() + bashful.GetDifficulty() + doc.GetDifficulty();
        value += sneezy.GetDifficulty() + happy.GetDifficulty() + grumpy.GetDifficulty();
        return (int)(value / 6);
    }

    public int GetGameLength() {
        return secondsPerHour * 6;
    }

    private IEnumerator GameOver() {
        yield return new WaitForSeconds(1.5f);
        gameOverImage.gameObject.SetActive(true);
        playAgain.SetActive(true);
    }

    public bool GetStudyUnlocked() {
        return docFree;
    }

    public bool GetBathroomUnlocked() {
        return sneezyFree;
    }

    public int ButtonNumber(int set) {
        int val = lastButtonPressed;
        if (set != -1) lastButtonPressed = set;
        return val;
    }

    public void StopOtherDwarves(Dwarf dwarf) {
        Debug.Log(dwarf);
        if (dwarf != Dwarf.sleepy) {
            sleepy.NotKiller();
            sleepy.PlayerDies();
        } else if (dwarf != Dwarf.bashful) {
            bashful.NotKiller();
            bashful.PlayerDies();
        } else if (doc.gameObject.activeInHierarchy && dwarf != Dwarf.doc) {
            doc.NotKiller();
            doc.PlayerDies();
        } else if (sneezy.gameObject.activeInHierarchy && dwarf != Dwarf.sneezy) {
            sneezy.NotKiller();
            sneezy.PlayerDies();
        } else if (happy.gameObject.activeInHierarchy && dwarf != Dwarf.happy) {
            happy.NotKiller();
            happy.PlayerDies();
        } else if (grumpy.gameObject.activeInHierarchy && dwarf != Dwarf.grumpy) {
            grumpy.NotKiller();
            grumpy.PlayerDies();
        }
    }

    public void SetAllPlayerDead() {
        sleepy.SetPlayerDead();
        bashful.SetPlayerDead();
        if (doc.gameObject.activeInHierarchy) doc.SetPlayerDead();
        if (sneezy.gameObject.activeInHierarchy) sneezy.SetPlayerDead();
        if (happy.gameObject.activeInHierarchy) happy.SetPlayerDead();
        if (grumpy.gameObject.activeInHierarchy) grumpy.SetPlayerDead();
    }
}
