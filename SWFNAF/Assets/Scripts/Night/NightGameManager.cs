using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightGameManager : MonoBehaviour
{
    public static NightGameManager S;

    public int secondsPerHour = 50;

    public GameObject door;
    public GameObject fire;

    public NightDwarfBehaviour dopey;
    public NightDwarfBehaviour sleepy;
    public NightDwarfBehaviour bashful;
    public NightDwarfBehaviour doc;
    public NightDwarfBehaviour sneezy;
    public NightDwarfBehaviour happy;
    public NightDwarfBehaviour grumpy;

    private Animator doorAnim;
    private NightDoorAudio doorAudio;

    private Location camAt = Location.none;
    private Location lastCam = Location.none;

    private bool doorClosed = false;
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
        doorAudio = door.GetComponent<NightDoorAudio>();
        fire.SetActive(false);
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
        ResetDwarves();
        night++;
        timePassed = 0;
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
            StartCoroutine(MakeTimePass());
        } else {
            NightEnd();
        }
    }

    private void StartNight() {
        StartCoroutine(MakeTimePass());
    }

    public void SwitchToBathroomCam() {
        camAt = Location.bathroom;
    }

    public void SwitchToBedroomCam() {
        camAt = Location.dwarfBedroom;
    }

    public void SwitchToKitchenCam() {
        camAt = Location.kitchen;
    }

    public void SwitchToMeatGrindersCam() {
        camAt = Location.meatGrinders;
    }

    public void SwitchToMinesCam() {
        camAt = Location.mines;
    }

    public void SwitchToStudyCam() {
        camAt = Location.study;
    }

    public void SwitchToWorkspaceCam() {
        camAt = Location.workspace;
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
        energy += amount;
    }

    public void CloseDoor() {
        doorAnim.SetBool("open", false);
        doorAudio.CloseDoor();
    }

    public void OpenDoor() {
        doorAnim.SetBool("open", true);
        doorAudio.OpenDoor();
    }

    public void LightFire() {
        fire.SetActive(true);
        fireLit = true;
    }

    private IEnumerator FireTimer() {
        yield return new WaitForSeconds(5f);
        fireLit = false;
        fire.SetActive(false);
    }
}
