using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MPUIKIT;
using Toiper;

public class UIScript : MonoBehaviour
{
    [SerializeField] NewGameData data;
    [SerializeField] NewCurrentData current;

    Toiper.Player.AbilitiesModule player;

    [SerializeField] GameObject levelCompleted;
    void Awake()
    {
        player = FindObjectOfType<Toiper.Player.AbilitiesModule>();
    }

    public void TrampolineClick()
    {
        player.UseTrampoline();
    }

    public void RollClick()
    {
        player.UseRoll();
    }


    public void ExpendAnimation(Animator animator)
    {
        animator.Play("Expended_Anim");
    }

    public void LevelCompleted()
    {
        levelCompleted.SetActive(true);
    }
}
