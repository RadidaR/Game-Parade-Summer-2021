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

        Timing.RunCoroutine(_UseSwing(swingHook), Segment.FixedUpdate);        
    }

    //IEnumerator<float> _Swing(GameObject hook)
    //{
    //    Debug.Log("Coroutine started");
    //    current.state = CurrentData.States.Swinging;
    //    eSwinging.Raise();
    //    joint.connectedBody = hook.GetComponent<Rigidbody2D>();
    //    joint.distance = data.ropeLength;
    //    joint.connectedAnchor = Vector2.zero;
    //    //joint.connectedAnchor = hook.transform.position;
    //    current.hookPosition = hook.transform.position;
    //    joint.enabled = true;
    //    lineRenderer.enabled = true;
    //    int updates = 0;

    //    while (current.swingInput == 1)
    //    {
    //        updates++;
    //        Debug.Log($"{updates}");
    //        lineRenderer.SetPosition(0, shootPoint.position);
    //        lineRenderer.SetPosition(1, hook.transform.position);
    //        //yield return Timing.WaitUntilDone(_Timer());
    //        yield return Timing.WaitForSeconds(Time.fixedDeltaTime);

    //        if (current.swingInput == 0)
    //            break;
    //    }

    //    joint.enabled = false;
    //    //Destroy(joint);
    //    lineRenderer.enabled = false;
    //    current.state = CurrentData.States.Airborne;
    //    //joint.connectedBody = null;
    //    Debug.Log("Coroutine ended");


    //}

    IEnumerator<float> _UseSwing(GameObject hook)
    {
        current.state = CurrentData.States.Swinging;
        eSwinging.Raise();
        //joint.connectedBody = hook.GetComponent<Rigidbody2D>();
        //joint.distance = data.ropeLength;
        //joint.connectedAnchor = Vector2.zero;
        //joint.connectedAnchor = hook.transform.position;
        current.hookPosition = hook.transform.position;
        //joint.enabled = true;
        //lineRenderer.enabled = true;



        Rigidbody2D hookRb = hook.GetComponentInChildren<Rigidbody2D>();

        if (transform.position.x > current.hookPosition.x)
        {
            if (transform.position.y >= current.hookPosition.y - 20f)
            {
                hookRb.rotation = 90f;
            }
            else
            {
                hookRb.rotation = 45f;
            }
        }
        else
        {
            if (transform.position.y >= current.hookPosition.y - 20f)
            {
                hookRb.rotation = -90f;
            }
            else
            {
                hookRb.rotation = -45f;
            }

            
        }
        yield return Timing.WaitForOneFrame;
        yield return Timing.WaitForOneFrame;
        yield return Timing.WaitForOneFrame;

        Transform swingTransform = hook.transform;

        foreach (Transform child in hook.transform)
        {
            foreach (Transform subChild in child)
            {
                if (subChild.gameObject.tag == "SwingSpot")
                {
                    swingTransform = subChild;
                    //current.swingPosition = subChild.position;
                    //current.swingRotation = subChild.rotation;
                }
            }
        }

        current.swingPosition = swingTransform.position;
        current.swingRotation = swingTransform.rotation;

        //Debug.Log("Gonnae wait now");
        yield return Timing.WaitUntilDone(_LerpToStartPoint(current.swingPosition), Segment.FixedUpdate);
        //Debug.Log("Done waiting");

        //int updates = 0;

        if (current.swingInput == 1)
        {
            hook.GetComponentInChildren<Pendulum>().enabled = true;

            yield return Timing.WaitUntilDone(_Swing(swingTransform), Segment.FixedUpdate);
        }



        //while (current.swingInput == 1)
        //{
        //    updates++;
        //    //Debug.Log($"{updates}");
        //    lineRenderer.SetPosition(0, shootPoint.position);
        //    lineRenderer.SetPosition(1, hook.transform.position);
        //    //yield return Timing.WaitUntilDone(_Timer());
        //    yield return Timing.WaitForSeconds(Time.fixedDeltaTime);

        //    if (current.swingInput == 0)
        //        break;
        //}

        //joint.enabled = false;
        //Destroy(joint);
        //lineRenderer.enabled = false;
        transform.localScale = transform.SetScale(x: current.direction);
        current.state = CurrentData.States.Airborne;
        //joint.connectedBody = null;


    }

    IEnumerator<float> _LerpToStartPoint(Vector3 newPos)
    {
        //Debug.Log("Start countdown");
        float timer = data.useSwingDuration;
        //lineRenderer.gameObject.SetActive(true);
        lineRenderer.enabled = true;

        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            yield return Timing.WaitForSeconds(Time.fixedDeltaTime);

            
            float t = 1 - (timer / data.useSwingDuration);
            transform.position = transform.LerpPosition(current.swingPosition, t);
            transform.rotation = transform.LerpEulerRotation(current.swingRotation, t);

            //Shoot rope
            lineRenderer.SetPosition(0, shootPoint.position);
            lineRenderer.SetPosition(1, shootPoint.LerpPosition(current.hookPosition, t * 2));


            if (timer <= 0)
                break;
        }
        //Debug.Log("Done counting");
    }

    IEnumerator<float> _Swing(Transform swingTransform)
    {
        Pendulum swing = swingTransform.GetComponentInParent<Pendulum>();
        if (current.direction == 1)
            swing.movingClockwise = true;
        else if (current.direction == -1)
            swing.movingClockwise = false;

        while (current.swingInput == 1)
        {
            yield return Timing.WaitForSeconds(Time.fixedDeltaTime);
            transform.position = swingTransform.position;
            transform.rotation = swingTransform.rotation;


            lineRenderer.SetPosition(0, shootPoint.position);
            lineRenderer.SetPosition(1, current.hookPosition);

            if (swing.movingClockwise)
            {
                current.movingRight = true;
                current.direction = 1;
            }
            else
            {
                current.movingRight = false;
                current.direction = -1;
            }

            if (current.swingInput == 0)
                break;
        }
        swing.enabled = false;
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
