using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseTrampoline : MonoBehaviour
{
    [SerializeField] CurrentData current;
    [SerializeField] GameObject trampoline;
    
    public void ExpendTrampoline()
    {
        if (!current.trampolineAvailable)
            return;

        Debug.Log("Parent null");
        trampoline.transform.SetParent(null);
        Debug.Log("setting position");
        trampoline.transform.position = transform.position.SetValues(x: transform.position.x + (3 * current.direction), y: transform.position.y - 0.5f);
        Debug.Log("activating");
        trampoline.SetActive(true);

        current.trampolineAvailable = false;
    }
}
