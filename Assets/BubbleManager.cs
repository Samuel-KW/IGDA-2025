using UnityEngine;
using System.Collections.Generic;

public static class BubbleManager
{
    public static HashSet<GameObject> playerBubbleList;
    public static List<List<GameObject>> bubbleTaskList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void Start()
    {
        playerBubbleList = new HashSet<GameObject>();
        bubbleTaskList = new List<List<GameObject>>();
    }

    public static void AddBubble(GameObject bubbleRef){
        playerBubbleList.Add(bubbleRef);
    }

    public static void RemoveBubble(GameObject bubbleRef){
        playerBubbleList.Remove(bubbleRef);
    }

    public static Vector3 GetMousePos(){
        var mouseWorldPos = Input.mousePosition;
        mouseWorldPos.z = 10.0f;
        return Camera.main.ScreenToWorldPoint(mouseWorldPos);
        
    }
}
