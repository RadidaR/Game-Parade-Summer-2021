using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameData data;
    [SerializeField] CurrentData current;

    private void Awake()
    {
        current.abilitiesUsed = 0;
        current.trampolineAvailable = true;
        current.rollAvailable = true;
        current.swingAvailable = true;
        current.state = CurrentData.States.Grounded;
        current.direction = 1;
    }
}
