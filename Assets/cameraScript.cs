using UnityEngine;

public class cameraScript : MonoBehaviour
{
    Vector2 offset = new Vector2(0f, 2f);
    float smoothTime = 1f;
    //Vector2 velocity = Vector2.zero;
    [SerializeField] private Transform target;
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 lastDirection = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target.position != lastPosition)
        {
            smoothTime = 0f;
        }
        smoothTime += Time.deltaTime;
        Vector2 targetPosition = (Vector2)target.position + offset + ((Vector2)lastDirection *10);
        Vector2 smoothedPosition = Vector2.Lerp((Vector2)transform.position, targetPosition, smoothTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10f);
        lastPosition = target.position;
        lastDirection = target.position - lastPosition;
        if(Vector3.Distance(targetPosition, transform.position) < 0.5f){
            transform.position = targetPosition;
        }
    }
}
