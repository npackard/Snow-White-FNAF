using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightPlayerLook : MonoBehaviour
{
    [Range(0, 1)]
    public float mouseSensitivity = 1f;
    public float smoothing = 2f;

    public float minY = -45f;
    public float maxY = 45f;

    private float change;
    private float smooth;

    private float mouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float sensitivity = mouseSensitivity / 10f;
        change = Input.GetAxisRaw("Mouse X");
        Vector2 mouseDir = Vector2.Scale(new Vector2(change, 0f), new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smooth = Mathf.Lerp(smooth, mouseDir.x, 1f / smoothing);
        mouse += smooth;
        transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(mouse, minY, maxY), transform.up);
    }
}

/*
ATTRIBUTIONS
First-Person Camera: https://github.com/jiankaiwang/FirstPersonController
*/
