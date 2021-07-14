using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class UseSwing : MonoBehaviour
{
    [SerializeField] GameData data;
    [SerializeField] CurrentData current;
    [SerializeField] LayerMask swingLayer;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] SpringJoint2D joint;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameEvent eSwinging;




    //public Rigidbody2D rb;

    //public Transform[] points; 
    //public float moveSpeed;

    //public Transform[] routes;
    //int nextRoute;
    //float tParam;
    //Vector2 position;
    //bool coroutineRunning;

    private void Start()
    {

        //nextRoute = 0;
        //tParam = 0;
        //coroutineRunning = false;
    }

    public void ExpendSwing()
    {
        if (!current.swingAvailable)
            return;

        if (!current.swingInReach)
            return;

        GameObject swingHook = Physics2D.OverlapCircle(transform.position, data.swingReach, swingLayer).gameObject;

        Timing.RunCoroutine(_Swing(swingHook), Segment.FixedUpdate);        
    }

    IEnumerator<float> _Swing(GameObject hook)
    {
        Debug.Log("Coroutine started");
        current.state = CurrentData.States.Swinging;
        eSwinging.Raise();
        joint.connectedBody = hook.GetComponent<Rigidbody2D>();
        joint.distance = data.ropeLength;
        joint.connectedAnchor = Vector2.zero;
        //joint.connectedAnchor = hook.transform.position;
        current.hookPosition = hook.transform.position;
        joint.enabled = true;
        lineRenderer.enabled = true;
        int updates = 0;

        while (current.swingInput == 1)
        {
            updates++;
            Debug.Log($"{updates}");
            lineRenderer.SetPosition(0, shootPoint.position);
            lineRenderer.SetPosition(1, hook.transform.position);
            //yield return Timing.WaitUntilDone(_Timer());
            yield return Timing.WaitForSeconds(Time.fixedDeltaTime);

            if (current.swingInput == 0)
                break;
        }

        joint.enabled = false;
        //Destroy(joint);
        lineRenderer.enabled = false;
        current.state = CurrentData.States.Airborne;
        //joint.connectedBody = null;
        Debug.Log("Coroutine ended");


    }

    IEnumerator<float> _Timer()
    {
        float timer = 5f;
        while (timer > 0)
        {
            Debug.Log($"{timer}");
            timer -= Time.fixedDeltaTime;
            yield return Timing.WaitForSeconds(Time.fixedDeltaTime);

            if (current.swingInput == 0)
                break;
        }
    }
}
