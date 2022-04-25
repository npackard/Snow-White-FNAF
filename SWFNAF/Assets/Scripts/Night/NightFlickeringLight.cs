using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class NightFlickeringLight : MonoBehaviour
{
    public float flickerLength = .5f;
    public float flickerThreshold = .8f;
    public float minLight = 0f;
    public float maxLight = 1f;
    
    public Light light;

    private bool flickering = false;
    private float timePassed = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // chance of flickering if not already flickering
        if (!flickering) {
            // determine chance
            float chance = Random.Range(0f, 1f);
            if (chance > flickerThreshold) {
                timePassed = 0f;
                flickering = true;
                light.intensity = minLight;
            }
        } else {
            // determine if light should be back on
            if (timePassed > flickerLength) {
                flickering = false;
                light.intensity = maxLight;
            } else {
                timePassed += Time.deltaTime;
            }
        }
    }
}
