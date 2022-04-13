using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NightGameManager : MonoBehaviour
{
    public static NightGameManager S;

    public int secondsPerHour = 50;
    
    public AudioSource fireAudio;
    public AudioSource fireIgnite;

    public GameObject door;
    public GameObject fire;

    public NightDwarfBehaviour dopey;
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

    private bool alive = true;
    private bool doorClosed = false;
    private bool eyesClosed = false;
    private bool fireLit = false;
    private bool ventClosed = false;
    private float energy = 0f;
    private int night;
    private int timePassed = 0;

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
        doorAnim = door.GetComponent<Animator>();
        fire.GetComponent<MeshRenderer>().enabled = false;
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

    public int GetNight() {
        return night;
    }

    // indicates which night is next
    private void NightEnd() {
        deathText.text = "YOU WIN";
        deathText.enabled = true;
        ResetDwarves();
        night++;
        timePassed = 0;
        StartCoroutine(MoveToDay());
    }

    private void ResetDwarves() {
        dopey.ResetDwarf();
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

    public void SwitchToBathroomCam() {
        lastCam = camAt;
        camAt = Location.bathroom;
    }

    public void SwitchToBedroomCam() {
        lastCam = camAt;
        camAt = Location.dwarfBedroom;
    }

    public void SwitchToKitchenCam() {
        lastCam = camAt;
        camAt = Location.kitchen;
    }

    public void SwitchToMeatGrindersCam() {
        lastCam = camAt;
        camAt = Location.meatGrinders;
    }

    public void SwitchToMinesCam() {
        lastCam = camAt;
        camAt = Location.mines;
    }

    public void SwitchToStudyCam() {
        lastCam = camAt;
        camAt = Location.study;
    }

    public void SwitchToWorkspaceCam() {
        lastCam = camAt;
        camAt = Location.workspace;
    }

    public void MirrorUp() {
        camAt = lastCam;
        Debug.Log(camAt);
        lastCam = camAt;
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
        Debug.Log("yo what?");
        doorClosed = !doorClosed;
        fireLit = false;
        fire.GetComponent<MeshRenderer>().enabled = false;
        fireAudio.loop = false;
        fireAudio.Stop();
        StopCoroutine(FireTimer());
        doorAnim.SetBool("open", !doorClosed);
    }

    public void LightFire() {
        fireIgnite.PlayOneShot(igniteClip);
        StartCoroutine(StartCrackle());
        fire.GetComponent<MeshRenderer>().enabled = true;
        doorClosed = false;
        doorAnim.SetBool("open", true);
        fireLit = true;
        StartCoroutine(FireTimer());
    }

    private IEnumerator FireTimer() {
        yield return new WaitForSeconds(5f);
        fireAudio.loop = false;
        fireAudio.Stop();
        fireLit = false;
        fire.GetComponent<MeshRenderer>().enabled = false;
    }

    public void Die() {
        alive = false;
        deathText.text = "GAME OVER";
        deathText.enabled = true;
    }

    public void StartLevelAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator MoveToDay() {
        PlayerPrefs.SetFloat("energy", energy);
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
