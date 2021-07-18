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

        GameObject swingHook = Physics2D.OverlapCircle(transform.position, data.swingReach + 1, swingLayer).gameObject;

        Timing.RunCoroutine(_UseSwing(swingHook), Segment.FixedUpdate);        
    }


    IEnumerator<float> _UseSwing(GameObject hook)
    {
        current.swingAvailable = false;
        current.abilitiesUsed++;
        current.state = CurrentData.States.Swinging;
        eSwinging.Raise();
        current.hookPosition = hook.transform.position;

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
                }
            }
        }

        current.swingPosition = swingTransform.position;
        current.swingRotation = swingTransform.rotation;

        yield return Timing.WaitUntilDone(_LerpToStartPoint(current.swingPosition), Segment.FixedUpdate);

        if (current.swingInput == 1)
        {
            hook.GetComponentInChildren<Pendulum>().enabled = true;

            yield return Timing.WaitUntilDone(_Swing(swingTransform), Segment.FixedUpdate);
        }

        transform.localScale = transform.SetScale(x: current.direction);
        current.state = CurrentData.States.Propelled;
        Timing.RunCoroutine(_Propelled(), Segment.FixedUpdate);
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
            transform.localScale = transform.SetScale(x: 1);
            transform.SetPositionAndRotation(swingTransform.position, swingTransform.rotation);
            current.swingAngle = swing.gameObject.transform.rotation.z;

            lineRenderer.SetPosition(0, shootPoint.position);
            lineRenderer.SetPosition(1, current.hookPosition);

            if (swing.movingClockwise)
            {
                yield return Timing.WaitForOneFrame;
                current.direction = 1;
            }
            else
            {
                yield return Timing.WaitForOneFrame;
                current.direction = -1;
            }

            if (current.swingInput == 0 || current.state != CurrentData.States.Swinging)
                break;
        }
        swing.enabled = false;
    }


    IEnumerator<float> _Propelled()
    {
        float timer = data.propelledDuration;
        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            yield return Timing.WaitForSeconds(Time.fixedDeltaTime);
            if (timer <= 0)
                break;
        }

        current.state = CurrentData.States.Airborne;
    }
}
