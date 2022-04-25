using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightAmbienceManager : MonoBehaviour
{
    public static NightAmbienceManager S;

    public AudioClip creak;
    public AudioClip footsteps;
    public AudioClip glass;
    public AudioClip howl;
    public AudioClip sneeze;
    public AudioClip table;

    private float waitTime;
    private int count = 0;
    private int gameTime;
    private int numberSounds;
    private int totalDifficulty;

    private AudioSource audio;

    private void Awake() {
        if (NightAmbienceManager.S) {
            Destroy(this.gameObject);
        } else {
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartSoundCycle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartSoundCycle() {
        count = 0;
        GetStats();
        StartCoroutine(PlaySounds());
    }

    private void GetStats() {
        totalDifficulty = NightGameManager.S.GetTotalDifficulty();
        gameTime = NightGameManager.S.GetGameLength();
        numberSounds = 20 - totalDifficulty;
        waitTime = gameTime / numberSounds;
    }

    private IEnumerator PlaySounds() {
        yield return new WaitForSeconds(waitTime);
        count++;
        float chance = Random.Range(0f, 6f);
        if (chance < 1) {
            audio.PlayOneShot(creak);
        } else if (chance < 2) {
            audio.PlayOneShot(footsteps);
        } else if (chance < 3) {
            audio.PlayOneShot(glass);
        } else if (chance < 4) {
            audio.PlayOneShot(howl);
        } else if (chance < 5) {
            audio.PlayOneShot(table);
        } else {
            audio.PlayOneShot(sneeze);
        }
        StartCoroutine(PlaySounds());
    }

    public void StopSounds() {
        StopAllCoroutines();
    }
}
