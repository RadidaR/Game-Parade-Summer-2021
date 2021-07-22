using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightScript : MonoBehaviour
{
    [SerializeField] NewCurrentData current;
    [SerializeField] GameObject highLight;

    // Update is called once per frame
    void Update()
    {
        if (current.swingAvailable)
            highLight.SetActive(current.swingInReach);
        else
            highLight.SetActive(false);
    }
}
