using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private float width, height, offset;
    private Vector3 startPos;

    public GameObject targetObject;
    public float speed = 0.01f;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object is not assigned");
            return;
        }

        offset = transform.localPosition.z; // Higher is faster
        
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;

        startPos = targetObject.transform.position;
    }

    void Update()
    {
        float diff = targetObject.transform.position.x - startPos.x;
        
        transform.position = new Vector3(startPos.x - (diff * speed * offset), transform.position.y, transform.position.z);
    }
}

