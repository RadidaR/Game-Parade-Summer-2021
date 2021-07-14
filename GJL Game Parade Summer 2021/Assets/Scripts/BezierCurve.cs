using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class BezierCurve : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform[] points; public float moveSpeed;

    public Transform[] routes;
    int nextRoute;
    float tParam;
    Vector2 position;
    bool coroutineRunning;
    // Start is called before the first frame update
    void Start()
    {
        nextRoute = 0;
        tParam = 0;
        coroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
