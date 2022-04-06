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

    public void BathroomCam() {
        NightSwitchCamera.S.SwitchToBathroomCam();
    }

    public void BedroomCam() {
        NightSwitchCamera.S.SwitchToBedroomCam();
    }

    public void KitchenCam() {
        NightSwitchCamera.S.SwitchToKitchenCam();
    }

    public void MeatGrinderCam() {
        NightSwitchCamera.S.SwitchToMeatGrindersCam();
    }

    public void MinesCam() {
        NightSwitchCamera.S.SwitchToMinesCam();
    }

    public void StudyCamera() {
        NightSwitchCamera.S.SwitchToStudyCam();
    }

    public void WorkspaceCam() {
        NightSwitchCamera.S.SwitchToWorkspaceCam();
    }
}
