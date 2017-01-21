using UnityEngine;
using System.Collections;

public class MovingFisherScript : MonoBehaviour
{
    public float moveSpeed;
    public Transform currentPoint;
    public Transform[] points;
    public int pointSelection;

    // Use this for initialization
    void Start()
    {
        currentPoint = points[pointSelection];
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * moveSpeed);
        if (transform.position == currentPoint.position)
        {
            pointSelection++;

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }

            currentPoint = points[pointSelection];
        }

    }
}
