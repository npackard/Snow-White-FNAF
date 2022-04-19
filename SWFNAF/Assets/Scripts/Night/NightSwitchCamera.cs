using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSwitchCamera : MonoBehaviour
{
    public static NightSwitchCamera S;

    public Material dwarfBedroom;
    public Material bathroom;
    public Material workshop;
    public Material unknown;
    public Material mineEntrance;
    public Material hallOne;
    public Material hallTwo;
    public Material livingRoom;
    public Material kitchen;

    private int counter = 0;
    
    private MeshRenderer meshRenderer;

    private void Awake() {
        if (NightSwitchCamera.S) {
            Destroy(this.gameObject);
        } else {
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToDwarfBedroomCam() {
        meshRenderer.material = dwarfBedroom;
        NightGameManager.S.SwitchToDwarfBedroom();
    }
    
    public void SwitchToBathroomCam() {
        meshRenderer.material = bathroom;
        NightGameManager.S.SwitchToBathroom();
    }

    public void SwitchToWorkshopCam() {
        meshRenderer.material = workshop;
        NightGameManager.S.SwitchToWorkshop();
    }

    public void SwitchToUnknownCam() {
        meshRenderer.material = unknown;
        NightGameManager.S.SwitchToUnknown();
    }

    public void SwitchToMineEntranceCam() {
        meshRenderer.material = mineEntrance;
        NightGameManager.S.SwitchToMineEntrance();
    }

    public void SwitchToHallOneCam() {
        meshRenderer.material = hallOne;
        NightGameManager.S.SwitchToHallOne();
    }

    public void SwitchToHallTwoCam() {
        meshRenderer.material = hallTwo;
        NightGameManager.S.SwitchToHallTwo();
    }

    public void SwitchToLivingRoomCam() {
        meshRenderer.material = livingRoom;
        NightGameManager.S.SwitchToLivingRoom();
    }

    public void SwitchToKitchenCam() {
        meshRenderer.material = kitchen;
        NightGameManager.S.SwitchToKitchen();
    }
}
