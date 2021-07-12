using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class UseTrampoline : MonoBehaviour
{
    [SerializeField] GameData data;
    [SerializeField] CurrentData current;
    [SerializeField] GameObject trampoline;
    [SerializeField] Transform spawnPoint;
    
    public void ExpendTrampoline()
    {
        if (!current.trampolineAvailable)
            return;

        if (current.state != CurrentData.States.Grounded)
            return;

        Timing.RunCoroutine(_UsingTrampolineDuration(), Segment.FixedUpdate);
    }

    IEnumerator<float> _UsingTrampolineDuration()
    {
        Debug.Log("Coroutine started");
        current.trampolineAvailable = false;
        current.state = CurrentData.States.UsingTrampoline;
        trampoline.transform.SetParent(null);
        trampoline.transform.position = spawnPoint.position;
        trampoline.transform.localScale = transform.localScale.SetValues(x: current.direction);
        trampoline.SetActive(true);

        float timer = data.useTrampolineDuration;
        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            Debug.Log($"Waiting {timer}");
            yield return Timing.WaitForSeconds(Time.fixedDeltaTime);
            if (timer <= 0)
                break;
        }

        current.state = CurrentData.States.Grounded;
        Debug.Log("Coroutine ended");
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnPoint == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPoint.position, 0.1f);
    }
}
