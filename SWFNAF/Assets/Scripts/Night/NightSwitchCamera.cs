using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSwitchCamera : MonoBehaviour
{
    public Material bathroom;
    public Material dwarfBedroom;
    public Material kitchen;
    public Material meatGrinders;
    public Material mines;
    public Material workspace;

    private int counter = 0;
    
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // temporary bad code for testing purposes
        if (Input.GetKeyDown(KeyCode.A)) {
            if (counter % 6 == 0) meshRenderer.material = dwarfBedroom;
            else if (counter % 6 == 1) meshRenderer.material = kitchen;
            else if (counter % 6 == 2) meshRenderer.material = meatGrinders;
            else if (counter % 6 == 3) meshRenderer.material = mines;
            else if (counter % 6 == 4) meshRenderer.material = workspace;
            else if (counter % 6 == 5) meshRenderer.material = bathroom;
            counter++;
        }

        //if (isActive) this.GetComponent<MeshRenderer>().material = bathroom;
    }
}
