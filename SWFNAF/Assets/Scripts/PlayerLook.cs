using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Range(0, 1)]
    public float mouseSensitivity = 1f;

    public float minY = -45f;
    public float maxY = 45f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float turnY = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        Debug.Log(transform.localRotation.y + turnY);
        transform.localRotation = Quaternion.Euler(0f, transform.localRotation.y + turnY, 0f);
    }
}
