using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DwarfBedroomCam() {
        NightSwitchCamera.S.SwitchToDwarfBedroomCam();
    }

    public void BathroomCam() {
        NightSwitchCamera.S.SwitchToBathroomCam();
    }

    public void WorkshopCam() {
        NightSwitchCamera.S.SwitchToWorkshopCam();
    }

    public void UnknownCam() {
        NightSwitchCamera.S.SwitchToUnknownCam();
    }

    public void MineEntranceCam() {
        NightSwitchCamera.S.SwitchToMineEntranceCam();
    }

    public void HallOneCam() {
        NightSwitchCamera.S.SwitchToHallOneCam();
    }

    public void HallTwoCam() {
        NightSwitchCamera.S.SwitchToHallTwoCam();
    }

    public void LivingRoomCam() {
        NightSwitchCamera.S.SwitchToLivingRoomCam();
    }

    public void KitchenCam() {
        NightSwitchCamera.S.SwitchToKitchenCam();
    }

    public void SwitchDoor() {
        Debug.Log("wanna be");
        NightGameManager.S.SwitchDoor();
    }

    public void LightFire() {
        NightGameManager.S.LightFire();
    }

    public void SwitchVent() {
        NightGameManager.S.SwitchVent();
    }
}
