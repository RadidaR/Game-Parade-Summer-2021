using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] NewGameData data;
    [SerializeField] NewCurrentData current;

    SceneManagerScript scene;
    AudioManager audioManager;

    private void Awake()
    {
        current.abilitiesUsed = 0;
        current.trampolineAvailable = true;
        current.rollAvailable = true;
        current.swingAvailable = true;
        current.state = NewCurrentData.States.Neutral;
        current.direction = 1;
        current.exitReached = false;
        scene = FindObjectOfType<SceneManagerScript>();
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlaySound("SFX_Level_Began");
    }

    public void LoadScene(int sceneNumber)
    {
        scene.LoadScene(sceneNumber);
    }

    public void RestartLevel() => scene.ReloadLevel();

    public void ReachExit()
    {
        current.exitReached = true;
        current.level++;

        scene.LoadNextLevel(current.level, data.transitionDelay);
    }

    public void PlayAudio(string audioName) => audioManager.PlaySound(audioName);
    public void StopAudio(string audioName) => audioManager.StopSound(audioName);
}
