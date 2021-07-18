using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MEC;

public class SceneManagerScript : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //Debug.Log("Updating");
    }
    public void LoadScene(int sceneNumber) => SceneManager.LoadScene(sceneNumber);

    public void LoadNextLevel(int nextLevel, float delay)
    {
        Debug.Log("scene manager 1");
        StartCoroutine(_NextLevel(nextLevel, delay));
    }

    IEnumerator _NextLevel(int nextLevel, float delay)
    {
        float t = delay;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return new WaitForSecondsRealtime(Time.deltaTime);
            Debug.Log("here");
            if (t <= 0)
                break;
        }
        Debug.Log("scene manager 2");
        SceneManager.LoadScene(nextLevel);
    }
}
