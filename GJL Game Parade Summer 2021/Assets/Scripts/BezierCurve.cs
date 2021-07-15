using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform[] points = new Transform[4];
    // Start is called before the first frame update
    void OnValidate()
    {
        if (gameObject.activeInHierarchy)
        {
            points[0] = transform.GetChild(0);
            points[1] = transform.GetChild(1);
            points[2] = transform.GetChild(2);
            points[3] = transform.GetChild(3);
        }
    }

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            Vector2 gizmoPos = Mathf.Pow(1 - t, 3) * points[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * points[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * points[2].position +
                Mathf.Pow(t, 3) * points[3].position;

            Gizmos.DrawSphere(gizmoPos, 0.1f);
        }

        Gizmos.DrawLine(points[0].position, points[1].position);

        Gizmos.DrawLine(points[2].position, points[3].position);
    }
}
