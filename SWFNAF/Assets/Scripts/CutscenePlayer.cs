using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutscenePlayer : MonoBehaviour
{
    private VideoPlayer vp;

    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        vp.Play();
        StartCoroutine(DisableVideo((float)vp.length));
    }

    private IEnumerator DisableVideo(float len)
    {
        yield return new WaitForSeconds(len);
        SceneManager.LoadScene(2);
    }
}
