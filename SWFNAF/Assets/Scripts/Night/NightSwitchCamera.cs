using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSwitchCamera : MonoBehaviour
{
    public static NightSwitchCamera S;

    public Material bathroom;
    public Material dwarfBedroom;
    public Material kitchen;
    public Material meatGrinders;
    public Material mines;
    public Material workspace;

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

    public void SwitchToBedroomCam() {
        meshRenderer.material = dwarfBedroom;
    }

    public void SwitchToBathroomCam() {
        meshRenderer.material = bathroom;
    }

    public void SwitchToKitchenCam() {
        meshRenderer.material = kitchen;
    }

    public void SwitchToMeatGrindersCam() {
        meshRenderer.material = meatGrinders;
    }

    public void SwitchToMinesCam() {
        meshRenderer.material = mines;
    }

    public void SwitchToWorkspaceCam() {
        meshRenderer.material = workspace;
    }
}
