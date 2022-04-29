using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSleep : MonoBehaviour
{
    public static NightSleep S;

    public GameObject doorButton;
    public GameObject fireplaceButton;
    public GameObject ventButton;

    private bool canControl = true;
    private float timer = 0f;

    private bool notifiedClose;
    private bool notifiedOpen;

    private Animator anim;

    private void Awake() {
        if (NightSleep.S) {
            Destroy(this.gameObject);
        } else {
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        doorButton.SetActive(true);
        fireplaceButton.SetActive(NightGameManager.S.GetStudyUnlocked());
        ventButton.SetActive(NightGameManager.S.GetBathroomUnlocked());
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl) {
            if (Input.GetKey(KeyCode.E)) {
                timer += Time.deltaTime;
                anim.SetBool("open", false);
                if (!notifiedClose) {
                    NightGameManager.S.CloseEyes();
                    notifiedClose = true;
                }
            } else {
                timer = 0f;
                anim.SetBool("open", true);
                if (notifiedClose) {
                    NightGameManager.S.OpenEyes();
                    notifiedClose = false;
                }
            }

            if (timer > 1f) {
                AddEnergy();
                timer = 0f;
            }
        } else {
            anim.SetBool("open", true);
            NightGameManager.S.OpenEyes();
        }
    }

    private void AddEnergy() {
        NightGameManager.S.AddEnergy(1f);
    }

    public void YesControl() {
        canControl = true;
    }

    public void NoControl() {
        canControl = false;
    }

    private IEnumerator ShowButtons() {
        yield return new WaitForSeconds(.4f);
        doorButton.SetActive(true);
        fireplaceButton.SetActive(NightGameManager.S.GetStudyUnlocked());
        ventButton.SetActive(NightGameManager.S.GetBathroomUnlocked());
    }

    public void CloseEyes() {
        StopAllCoroutines();
        doorButton.SetActive(false);
        fireplaceButton.SetActive(false);
        ventButton.SetActive(false);
    }

    public void OpenEyes() {
        StartCoroutine(ShowButtons());
    }
}
