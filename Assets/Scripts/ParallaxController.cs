using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private float width, height, offset;
    private Vector3 startPos;
    private GameObject targetObject;

    public float speed = 0.01f;

    void Start()
    {
        targetObject = GameObject.FindWithTag("Player");

        if (targetObject == null)
        {
            Debug.LogError("Player Object is not defined");
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

