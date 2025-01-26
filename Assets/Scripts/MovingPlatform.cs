using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed = 0.1f;
    public Vector3 endPos = Vector3.zero;

    private Vector3 startPos = Vector3.zero;
    private int platformDirection = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {

        float step = speed * Time.deltaTime;


        // Check if direction should change
        if (transform.position == endPos)
        {
            platformDirection = -1;
        }
        else if (transform.position == startPos)
        {
            platformDirection = 1;
        }

        // Move platform
        if (platformDirection == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
        }

    }

    private void OnDrawGizmos()
    {
        // Visualize the end positions in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPos, 0.5f);
    }
}
