using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class UseTrampoline : MonoBehaviour
{
    [SerializeField] GameData data;
    [SerializeField] CurrentData current;
    [SerializeField] GameObject trampoline;
    //[SerializeField] Transform trampolineScale;
    [SerializeField] Transform spawnPoint;

    [SerializeField] GameEvent eTrampolineUsed;

    private void Update()
    {
        if (current.state == CurrentData.States.Done)
            Timing.KillCoroutines();
    }
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
        current.trampolineAvailable = false;
        current.abilitiesUsed++;
        eTrampolineUsed.Raise();
        current.state = CurrentData.States.UsingTrampoline;
        trampoline.transform.position = spawnPoint.position;
        trampoline.transform.SetParent(null);
        trampoline.transform.localScale = trampoline.transform.localScale.SetValues(x: current.direction * Mathf.Abs(trampoline.transform.localScale.x));
        trampoline.SetActive(true);

        //trampoline.transform.localScale = trampoline.transform.localScale.SetValues(x: trampoline.transform.localScale.x * current.direction);
        float timer = data.useTrampolineDuration;
        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            yield return Timing.WaitForSeconds(Time.fixedDeltaTime);
            if (timer <= 0)
                break;
        }
        
        current.state = CurrentData.States.Grounded;
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnPoint == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPoint.position, 0.1f);
    }
}
