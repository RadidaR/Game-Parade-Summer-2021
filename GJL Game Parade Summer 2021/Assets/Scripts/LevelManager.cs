using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameData data;
    [SerializeField] CurrentData current;

    SceneManagerScript scene;

    private void Awake()
    {
        current.abilitiesUsed = 0;
        current.trampolineAvailable = true;
        current.rollAvailable = true;
        current.swingAvailable = true;
        current.state = CurrentData.States.Grounded;
        current.direction = 1;
        current.exitReached = false;
        scene = FindObjectOfType<SceneManagerScript>();
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

        scene.LoadNextLevel(current.level, data.loadNextLevelDelay);
    }
}
