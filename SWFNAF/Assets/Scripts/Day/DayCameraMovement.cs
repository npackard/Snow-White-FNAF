using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayCameraMovement : MonoBehaviour
{
    public static DayCameraMovement instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<DayCameraMovement>();
            }

            return m_instance;
        }
    }

    public static DayCameraMovement m_instance;

    public VolumeProfile volume;
    //private DepthOfField dof;
    //private LensDistortion ld;
    //public float ldMax = 0.3f;

    public DayCameraLocation start;
    public DayCameraLocation curr;

    private Vector3 velocity;
    private float xVel;
    private float yVel;
    private bool camMoving;

    public float speedH = 2f;
    public float speedV = 2f;

    private float horizAng;
    private float vertAng;
    public float maxVertAng = 50;

    private GameObject lastHit;
    private bool canTouch;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        transform.position = start.gameObject.transform.position;
        curr = start;
        curr.OnThisCam();

        Cursor.lockState = CursorLockMode.Locked;

        //volume.TryGet(out dof);
        //volume.TryGet(out ld);
        //ld.intensity.Override(0f);
    }

    void Update()
    {
        if (camMoving) return;

        horizAng += speedH * Input.GetAxis("Mouse X");
        if (!(Input.GetAxis("Mouse Y") < 0 && vertAng > maxVertAng) && !(Input.GetAxis("Mouse Y") > 0 && vertAng < -maxVertAng))
        {
            vertAng -= speedV * Input.GetAxis("Mouse Y");
        }
        transform.eulerAngles = new Vector3(vertAng, horizAng, 0);

        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        int layer_mask = 1 << curr.gameObject.layer;
        if (Physics.Raycast(ray, out hit, 10, layer_mask))
        {
            if (hit.transform.gameObject.tag == "Interactable" ||
                hit.transform.gameObject.tag == "Gemstone")
            {
                canTouch = true;
                lastHit = hit.transform.gameObject;
                DayUIManager.instance.PanelInteractableOn();
            }
        }
        else 
        { 
            canTouch = false;
            lastHit = null;
            DayUIManager.instance.PanelInteractableOff();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (curr.left) Move(curr.left);
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            if (curr.right) Move(curr.right);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (curr.front) Move(curr.front);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (curr.back) Move(curr.back);
        }
        else if (canTouch && Input.GetKeyDown(KeyCode.F))
        {
            // Display interactable UI

            //lastHit.GetComponent<>.Interact();

            if (lastHit.gameObject.tag == "Gemstone")
            {
                DayGameManager.instance.GetGem(lastHit.GetComponent<DayGemstone>().dwarfIndex);
                Destroy(lastHit);
                canTouch = false;
            }
        }
    }

    private void Move(DayCameraLocation target)
    {
        camMoving = true;

        curr.LeaveThisCam();
        curr = target;

        StartCoroutine(MovePos(target.gameObject.transform));
    }

    private IEnumerator MovePos(Transform target)
    {
        while (Mathf.Abs(target.transform.eulerAngles.x - transform.eulerAngles.x) > 0.2f)
        {
            float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, target.transform.eulerAngles.x, ref yVel, 0.1f);
            transform.rotation = Quaternion.Euler(xAngle, transform.eulerAngles.y, 0);
            yield return null;
        }
        while (Mathf.Abs(target.transform.eulerAngles.y - transform.eulerAngles.y) > 0.2f)
        {
            float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.transform.eulerAngles.y, ref yVel, 0.1f);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, yAngle, 0);
            yield return null;
        }
        /*while (Mathf.Abs(target.transform.eulerAngles.x - transform.eulerAngles.x) > 0.2f || 
                Mathf.Abs(target.transform.eulerAngles.y - transform.eulerAngles.y) > 0.2f)
        {
            float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, target.transform.eulerAngles.x, ref yVel, 0.1f);
            float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.transform.eulerAngles.y, ref yVel, 0.1f);
            transform.rotation = Quaternion.Euler(xAngle, 0, 0);
            yield return null;
        }*/
        while (Mathf.Abs(target.position.x - transform.position.x) > 0.2f || Mathf.Abs(target.position.y - transform.position.y) > 0.2f
                                                                        || Mathf.Abs(target.position.z - transform.position.z) > 0.2f)
        {
            transform.position = Vector3.SmoothDamp(transform.position,
                                new Vector3(target.position.x, target.position.y, target.position.z), ref velocity, 0.1f);
            yield return null;
        }

        transform.position = target.transform.position;
        vertAng = target.eulerAngles.x;
        horizAng = target.eulerAngles.y;
        curr.OnThisCam();
        camMoving = false;
    }
}
