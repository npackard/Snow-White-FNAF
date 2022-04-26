using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MineCameraMovement : MonoBehaviour
{
    public static MineCameraMovement instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<MineCameraMovement>();
            }

            return m_instance;
        }
    }

    public static MineCameraMovement m_instance;

    public VolumeProfile volume;

    public DayCameraLocation start;
    public DayCameraLocation curr;

    private Vector3 velocity;
    private bool camMoving;

    public float speedH = 2f;
    public float speedV = 2f;

    private float horizAng;
    private float vertAng;
    public float maxVertAng = 50;

    private GameObject lastHit;
    private bool canTouch;

    private int temp_layer = 0;
    private int highlightMask;

    private int gemCount = 0;

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
        transform.rotation = Quaternion.Euler(0, 90, 0);
        curr = start;

        curr.OnThisCam();

        highlightMask = LayerMask.NameToLayer("Highlight");

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (camMoving) return;

        horizAng += speedH * Input.GetAxis("Mouse X");
        if (!(Input.GetAxis("Mouse Y") < 0 && vertAng > maxVertAng) && !(Input.GetAxis("Mouse Y") > 0 && vertAng < -maxVertAng))
        {
            vertAng -= speedV * Input.GetAxis("Mouse Y");
        }
        transform.eulerAngles = new Vector3(vertAng, 90 + horizAng, 0);

        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        int layer_mask = (1 << curr.gameObject.layer) | LayerMask.GetMask("Highlight");

        if (Physics.Raycast(ray, out hit, 60, layer_mask))
        {
            if (hit.transform.gameObject.tag == "Pedestal" ||
                hit.transform.gameObject.tag == "Mirror")
            {
                if (lastHit && hit.transform.gameObject != lastHit) lastHit.layer = temp_layer;
                if (hit.transform.gameObject.layer != highlightMask) temp_layer = hit.transform.gameObject.layer;
                hit.transform.gameObject.layer = highlightMask;

                canTouch = true;
                lastHit = hit.transform.gameObject;
                MineUIManager.instance.PanelInteractableOn();
            }
            else
            {
                if (lastHit) lastHit.layer = temp_layer;
                canTouch = false;
                lastHit = null;
                MineUIManager.instance.PanelInteractableOff();
            }
        }
        else
        {
            if (lastHit) lastHit.layer = temp_layer;
            canTouch = false;
            lastHit = null;
            MineUIManager.instance.PanelInteractableOff();
        }

        if (canTouch && Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!lastHit) return;
            if (lastHit.tag == "Pedestal")
            {
                lastHit.GetComponent<MinePedestal>().ActivateGem();
                canTouch = false;
                MineUIManager.instance.PanelInteractableOff();
                gemCount++;
                if (gemCount == 7) MineGameManager.instance.AllGems();
            }
            else if (lastHit.tag == "Mirror")
            {
                lastHit.GetComponent<MineMirror>().InteractMirror();
                canTouch = false;
                MineUIManager.instance.PanelInteractableOff();
            }
        }
    }
}
