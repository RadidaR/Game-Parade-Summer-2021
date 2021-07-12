using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRoll : MonoBehaviour
{
    [SerializeField] CurrentData current;

    public void ExpendRoll()
    {
        if (!current.dashAvailable)
            return;


    }
}
