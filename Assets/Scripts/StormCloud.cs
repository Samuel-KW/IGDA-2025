using System;
using UnityEngine;

public class StormCloud : MonoBehaviour
{

    public float minY = 500;
    public GameObject targetObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (targetObject == null)
        {
            targetObject = GameObject.FindWithTag("Player");

            if (targetObject == null )
            {
                Debug.LogError("Target Object is not defined and Player does not exist");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = targetObject.transform.position;
        transform.position = new Vector3(pos.x, Math.Max(pos.y, minY), transform.position.z);
    }
}
