using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MEC;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;
    [SerializeField] NewCurrentData current;
    AudioManager audioManager;

    void Awake()
    {
        Debug.Log(SceneManager.sceneCount);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        audioManager = FindObjectOfType<AudioManager>();
        DontDestroyOnLoad(gameObject);
    }

        private void Update()
    {
        //Debug.Log("Updating");
    }
    public void LoadScene(int sceneNumber)
    {
        if (sceneNumber == 0)
        {
            audioManager.StopSound("Music_Gameplay");
            audioManager.PlaySound("Music_Menu");
        }
        else
        {
            current.mainMenu = false;
        }

        SceneManager.LoadScene(sceneNumber);
    }

    public void ReloadLevel() => SceneManager.LoadScene(current.level);

    public void LoadNextLevel(int nextLevel, float delay)
    {
        //Debug.Log("scene manager 1");
        StartCoroutine(_NextLevel(nextLevel, delay));
    }

    IEnumerator _NextLevel(int nextLevel, float delay)
    {
        float t = delay;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return new WaitForSecondsRealtime(Time.deltaTime);
            //Debug.Log("here");
            if (t <= 0)
                break;
        }
        //Debug.Log("scene manager 2");

        if (nextLevel == 7)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
