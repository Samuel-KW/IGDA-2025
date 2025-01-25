using UnityEngine;
using System.Collections.Generic;

public class Cookie : MonoBehaviour
{
    [SerializeField] private int neededBubbles;
    [SerializeField] List<GameObject> bubbles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
