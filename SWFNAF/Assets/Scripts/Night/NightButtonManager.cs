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
        if (PlayerPrefs.GetInt("Key3") == 1) NightSwitchCamera.S.SwitchToDwarfBedroomCam();
        else NightSwitchCamera.S.RejectedSound();
    }

    public void BathroomCam() {
        if (PlayerPrefs.GetInt("Key2") == 1) NightSwitchCamera.S.SwitchToBathroomCam();
        else NightSwitchCamera.S.RejectedSound();
    }

    public void WorkshopCam() {
        if (PlayerPrefs.GetInt("Key4") == 1) NightSwitchCamera.S.SwitchToWorkshopCam();
        else NightSwitchCamera.S.RejectedSound();
    }

    public void UnknownCam() {
        if (PlayerPrefs.GetInt("Key1") == 1) NightSwitchCamera.S.SwitchToUnknownCam();
        else NightSwitchCamera.S.RejectedSound();
    }

    public void MineEntranceCam() {
        NightSwitchCamera.S.RejectedSound();
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
        NightGameManager.S.SwitchDoor();
    }

    public void LightFire() {
        NightGameManager.S.LightFire();
    }

    public void SwitchVent() {
        NightGameManager.S.SwitchVent();
    }

    public void PlayAgain() {
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
    }
}
