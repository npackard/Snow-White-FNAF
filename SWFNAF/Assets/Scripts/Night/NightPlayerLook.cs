using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightPlayerLook : MonoBehaviour
{
    public float minY = 182.5f;
    public float maxY = 242.5f;
    public float speed = .5f;

    private float change;
    private float smooth;
    private float mouse;
    private float newY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.y > Screen.height / 5) {
            if (Input.mousePosition.x < Screen.width / 5) {
                newY -= speed;
            } else if (Input.mousePosition.x > 4 * Screen.width / 5) {
                newY += speed;
            }
        }
        if (newY < 0) newY = 360 + newY;
        newY = Mathf.Clamp(newY, minY, maxY);
        transform.rotation = Quaternion.Euler(transform.rotation.x, newY, transform.rotation.z);

    }
}
