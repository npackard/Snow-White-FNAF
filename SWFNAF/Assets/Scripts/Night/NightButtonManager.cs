using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (NightGameManager.S.ButtonNumber(-1) != 0) {
            if (PlayerPrefs.GetInt("Key3") == 1) NightSwitchCamera.S.SwitchToDwarfBedroomCam();
            else NightSwitchCamera.S.RejectedSound();
            NightGameManager.S.ButtonNumber(0);
        }
    }

    public void BathroomCam() {
        if (NightGameManager.S.ButtonNumber(-1) != 1) {
            if (PlayerPrefs.GetInt("Key2") == 1) NightSwitchCamera.S.SwitchToBathroomCam();
            else NightSwitchCamera.S.RejectedSound();
            NightGameManager.S.ButtonNumber(1);
        }
    }

    public void WorkshopCam() {
        if (NightGameManager.S.ButtonNumber(-1) != 2) {
            if (PlayerPrefs.GetInt("Key4") == 1) NightSwitchCamera.S.SwitchToWorkshopCam();
            else NightSwitchCamera.S.RejectedSound();
            NightGameManager.S.ButtonNumber(2);
        }
    }

    public void UnknownCam() {
        if (NightGameManager.S.ButtonNumber(-1) != 3) {
            if (PlayerPrefs.GetInt("Key1") == 1) NightSwitchCamera.S.SwitchToUnknownCam();
            else NightSwitchCamera.S.RejectedSound();
            NightGameManager.S.ButtonNumber(3);
        }
    }

    public void MineEntranceCam() {
        if (NightGameManager.S.ButtonNumber(-1) != 4) {
            NightSwitchCamera.S.RejectedSound();
            NightGameManager.S.ButtonNumber(4);
        }
    }

    public void HallOneCam() {
        if (NightGameManager.S.ButtonNumber(-1) != 5) {
            NightSwitchCamera.S.SwitchToHallOneCam();
            NightGameManager.S.ButtonNumber(5);
        }
    }

    public void HallTwoCam() {
        if (NightGameManager.S.ButtonNumber(-1) != 6) {
            NightSwitchCamera.S.SwitchToHallTwoCam();
            NightGameManager.S.ButtonNumber(6);
        }
    }

    public void LivingRoomCam() {
        if (NightGameManager.S.ButtonNumber(-1) != 7) {
            NightSwitchCamera.S.SwitchToLivingRoomCam();
            NightGameManager.S.ButtonNumber(7);
        }
    }

    public void KitchenCam() {
        if (NightGameManager.S.ButtonNumber(-1) != 8) {
            NightSwitchCamera.S.SwitchToKitchenCam();
            NightGameManager.S.ButtonNumber(8);
        }
    }

    public void SwitchDoor() {
        if (NightGameManager.S.ButtonNumber(-1) != 9) {
            NightGameManager.S.SwitchDoor();
            NightGameManager.S.ButtonNumber(9);
        }
    }

    public void LightFire() {
        if (NightGameManager.S.ButtonNumber(-1) != 10) {
            NightGameManager.S.LightFire();
            NightGameManager.S.ButtonNumber(10);
        }
    }

    public void SwitchVent() {
        if (NightGameManager.S.ButtonNumber(-1) != 11) {
            NightGameManager.S.SwitchVent();
            NightGameManager.S.ButtonNumber(11);
        }
    }

    public void PlayAgain() {
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
    }
}
