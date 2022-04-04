using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCameraMovement : MonoBehaviour
{
    //Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.velocity = new Vector3(0, 0, 20);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, 10 * Time.deltaTime);
    }
}
