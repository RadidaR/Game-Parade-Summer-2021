using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] NewCurrentData current;
    [SerializeField] NewGameData data;


    //public float moveSpeed;
    //public float leftAngle;
    //public float rightAngle;

    public bool movingClockwise;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //movingClockwise = true;
    }

    //private void OnEnable()
    //{
    //    if (current.direction == 1)
    //    {
    //        movingClockwise = false;
    //    }
    //    else if (current.direction == -1)
    //    {
    //        movingClockwise = true;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {

        Move();
    }

    public void ChangeMoveDir()
    {
        if (transform.rotation.z > data.swingAngle)
        {
            movingClockwise = true;
        }
        if (transform.rotation.z < -data.swingAngle)
        {
            movingClockwise = false;
        }

    }

    public void Move()
    {
        ChangeMoveDir();
        //Debug.Log($"{(transform.rotation.z / data.swingAngle)}");
        //Debug.Log($"{1 - (transform.rotation.z / data.swingAngle)}");
        //float angleModifier = Mathf.Abs(transform.rotation.z / data.swingAngle);
        //Debug.Log($"{angleModifier}");
            if (!movingClockwise)
            {
            //rb2d.angularVelocity = data.swingSpeed;
            if (transform.rotation.z < data.swingAngle * 0.5f)
                rb2d.AddTorque(data.swingSpeed);
            else
                movingClockwise = true;
                //rb2d.AddTorque(-data.swingSpeed / 2);

                //if (transform.rotation.z > data.swingAngle * 0.25f)
                //    movingClockwise = true;
                //else


                //Debug.Log("Moving Clockwise");
                //if (transform.rotation.z < -data.swingAngle * angleModifier)
                //{
                //    Debug.Log($"Rotation: {transform.rotation.z} / {-data.swingAngle}");
                //    rb2d.angularVelocity = data.swingSpeed * (1 - angleModifier);
                //}

                //if (transform.rotation.z < -data.swingAngle * angleModifier)
                //{
                //    rb2d.angularVelocity = data.swingSpeed * (1 - angleModifier);
                //}
                //else if (transform.rotation.z > data.swingAngle * angleModifier)
                //{
                //    rb2d.angularVelocity = data.swingSpeed * (1 - angleModifier);
                //}


                //if (transform.rotation.z < -data.swingAngle * 0.9f)
                //{
                //    rb2d.angularVelocity = data.swingSpeed * 0.3f;
                //}
                //else if (transform.rotation.z < -data.swingAngle * 0.8f)
                //{
                //    rb2d.angularVelocity = data.swingSpeed * 0.4f;
                //}
                //else if (transform.rotation.z < -data.swingAngle * 0.7f)
                //{
                //    rb2d.angularVelocity = data.swingSpeed * 0.6f;
                //}
                //else if (transform.rotation.z < -data.swingAngle * 0.6f)
                //{
                //    rb2d.angularVelocity = data.swingSpeed * 0.8f;
                //}
                //else if (transform.rotation.z < 0)
                //{
                //    rb2d.angularVelocity = data.swingSpeed;
                //}
            }

        if (movingClockwise)
        {
            //rb2d.angularVelocity = -data.swingSpeed;
            //if (transform.rotation.z > 0)

            if (transform.rotation.z > -data.swingAngle * 0.5f)
                rb2d.AddTorque(-data.swingSpeed);
            else
                movingClockwise = false;
                //rb2d.AddTorque(data.swingAngle / 2);
            

            //Debug.Log("Moving Anti-Clockwise");
            //if (transform.rotation.z > data.swingAngle * angleModifier)
            //{
            //    Debug.Log($"Rotation: {transform.rotation.z} / {data.swingAngle}");
            //    //float angleModifier = Mathf.Abs(1 - (transform.rotation.z / data.swingAngle));
            //    rb2d.angularVelocity = -data.swingSpeed * (1 - angleModifier);
            //}

            //if (transform.rotation.z > data.swingAngle * angleModifier)
            //{
            //    rb2d.angularVelocity = -data.swingSpeed * (1 - angleModifier);
            //}
            //else if (transform.rotation.z < -data.swingAngle * angleModifier)
            //{
            //    rb2d.angularVelocity = -data.swingSpeed * (1 - angleModifier);
            //}



            //if (transform.rotation.z > data.swingAngle * 0.9f)
            //{
            //    rb2d.angularVelocity = -data.swingSpeed * 0.4f;
            //}
            //else if (transform.rotation.z > data.swingAngle * 0.8f)
            //{
            //    rb2d.angularVelocity = -data.swingSpeed * 0.4f;
            //}
            //else if (transform.rotation.z > data.swingAngle * 0.7f)
            //{
            //    rb2d.angularVelocity = -data.swingSpeed * 0.6f;
            //}
            //else if (transform.rotation.z > data.swingAngle * 0.6f)
            //{
            //    rb2d.angularVelocity = -data.swingSpeed * 0.8f;
            //}
            //else if (transform.rotation.z > 0)
            //{
            //    rb2d.angularVelocity = -data.swingSpeed;
            //}

        }
    }
}