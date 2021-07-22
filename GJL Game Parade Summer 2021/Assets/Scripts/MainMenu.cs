using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] NewCurrentData current;
    [SerializeField] SceneManagerScript scene;
    [SerializeField] float delayDuration;

    AudioManager audioManager;

    private void Awake()
    {
        //current.state = NewCurrentData.States.Done;
        current.level = 0;
        current.mainMenu = true;
        scene = FindObjectOfType<SceneManagerScript>();
        audioManager = FindObjectOfType<AudioManager>();
        //audioManager.PlaySound("Music_Menu");
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

    public void PlayAudio(string audioName) => audioManager.PlaySound(audioName);
    public void StopAudio(string audioName) => audioManager.StopSound(audioName);
}
