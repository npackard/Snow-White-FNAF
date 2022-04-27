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

    public DayCameraLocation firstDay;
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

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("DayCount") == 0)
        {
            transform.position = firstDay.gameObject.transform.position;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            curr = firstDay;
        }
        else
        {
            transform.position = start.gameObject.transform.position;
            transform.rotation = Quaternion.Euler(0, 90, 0);
            curr = start;
        }

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
        int layer_mask = (1 << curr.gameObject.layer) | LayerMask.GetMask("CamLocations", "Highlight");

        if (Physics.Raycast(ray, out hit, 60, layer_mask))
        {
            if (hit.transform.gameObject.tag == "Gemstone" ||
                hit.transform.gameObject.tag == "Key" ||
                hit.transform.gameObject.tag == "Camera" ||
                hit.transform.gameObject.tag == "FirstDay")
            {
                if (lastHit && hit.transform.gameObject != lastHit) lastHit.layer = temp_layer;
                if (hit.transform.gameObject.layer != highlightMask) temp_layer = hit.transform.gameObject.layer;
                hit.transform.gameObject.layer = highlightMask;

                canTouch = true;
                lastHit = hit.transform.gameObject;
                DayUIManager.instance.PanelInteractableOn();
            } else if (hit.transform.gameObject.tag == "Door")
            {
                if (hit.transform.gameObject.GetComponent<DayDoor>().open) return;
                else
                {
                    if (lastHit && hit.transform.gameObject != lastHit) lastHit.layer = temp_layer;
                    if (hit.transform.gameObject.layer != highlightMask) temp_layer = hit.transform.gameObject.layer;
                    hit.transform.gameObject.layer = highlightMask;

                    canTouch = true;
                    lastHit = hit.transform.gameObject;
                    DayUIManager.instance.PanelInteractableOn();
                }
            } else
            {
                if (lastHit) lastHit.layer = temp_layer;
                canTouch = false;
                lastHit = null;
                DayUIManager.instance.PanelInteractableOff();
            }
        }
        else 
        {
            if (lastHit) lastHit.layer = temp_layer;
            canTouch = false;
            lastHit = null;
            DayUIManager.instance.PanelInteractableOff();
        }
        
        // check for Go To Bed btn
        if (Physics.Raycast(ray, out hit, 20, layer_mask))
        {
            if (hit.transform.gameObject.tag == "Interactable" ||
                hit.transform.gameObject.tag == "Portal")
            {
                if (lastHit && hit.transform.gameObject != lastHit) lastHit.layer = temp_layer;
                if (hit.transform.gameObject.layer != highlightMask) temp_layer = hit.transform.gameObject.layer;
                hit.transform.gameObject.layer = highlightMask;

                canTouch = true;
                lastHit = hit.transform.gameObject;
                DayUIManager.instance.PanelInteractableOn();
            }
        }

        if (canTouch && Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!lastHit) return;
            if (lastHit.tag == "Gemstone")
            {
                lastHit.GetComponent<DayGemstone>().PlayAudio();
                int gemInd = lastHit.GetComponent<DayGemstone>().gemIndex;
                if (gemInd == 0) DayGameManager.instance.FirstDayCollected();
                PlayerPrefs.SetInt("Gem" + gemInd.ToString(), 1);
                canTouch = false;
                DayUIManager.instance.PanelInteractableOff();
            }
            else if (lastHit.tag == "Key")
            {
                lastHit.GetComponent<DayKey>().PlayAudio();
                int keyInd = lastHit.gameObject.GetComponent<DayKey>().keyIndex;
                PlayerPrefs.SetInt("Key" + keyInd.ToString(), 1);
                DayGameManager.instance.GetKey(keyInd);
                canTouch = false;
                DayUIManager.instance.PanelInteractableOff();
            }
            else if (lastHit.tag == "Door")
            {
                if (DayGameManager.instance.CheckKey(lastHit.gameObject.GetComponent<DayDoor>().doorIndex)) {
                    lastHit.gameObject.GetComponent<DayDoor>().OpenDoor();
                } else
                {
                    lastHit.gameObject.GetComponent<DayDoor>().LockedDoor();
                }
                canTouch = false;
            }
            else if (lastHit.tag == "Camera")
            {
                canTouch = false;
                DayUIManager.instance.PanelInteractableOff();
                Move(lastHit.gameObject.transform.parent.gameObject);
            }
            else if (lastHit.tag == "Interactable")
            {
                canTouch = false;
                DayUIManager.instance.PanelInteractableOff();
                GameManager.instance.EndDay();
            }
            else if (lastHit.tag == "Portal")
            {
                canTouch = false;
                DayUIManager.instance.PanelInteractableOff();
                GameManager.instance.LoadMine();
            }
            else if (lastHit.tag == "FirstDay")
            {
                canTouch = false;
                DayUIManager.instance.PanelInteractableOff();
                lastHit.gameObject.GetComponent<DayFirstInteractable>().Interact();
            }
        }
    }

    private void Move(GameObject target)
    {
        camMoving = true;

        DayUIManager.instance.PanelInteractableOff();

        curr.LeaveThisCam();
        curr = target.GetComponent<DayCameraLocation>();

        StartCoroutine(MovePos(target.transform));
        lastHit = null;
    }

    private IEnumerator MovePos(Transform target)
    {
        while (Mathf.Abs(target.position.x - transform.position.x) > 0.2f || Mathf.Abs(target.position.y - transform.position.y) > 0.2f
                                                                        || Mathf.Abs(target.position.z - transform.position.z) > 0.2f)
        {
            transform.position = Vector3.SmoothDamp(transform.position,
                                new Vector3(target.position.x, target.position.y, target.position.z), ref velocity, 0.1f);
            yield return null;
        }

        transform.position = target.transform.position;

        curr.OnThisCam();
        camMoving = false;
    }
}
