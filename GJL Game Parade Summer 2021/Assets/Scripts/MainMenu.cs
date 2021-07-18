using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] CurrentData current;
    [SerializeField] SceneManagerScript scene;
    [SerializeField] float delayDuration;

    private void Awake()
    {
        current.state = CurrentData.States.Done;
        current.level = 0;
        scene = FindObjectOfType<SceneManagerScript>();
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(LoadLevel(level, delayDuration));
        //scene.LoadScene()
    }

    IEnumerator LoadLevel(int level, float delay)
    {
        yield return new WaitForSeconds(delay);
        current.level = level;
        scene.LoadScene(level);
    }
}
