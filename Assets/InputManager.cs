using UnityEngine;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.CSharp;
public class InputManager : MonoBehaviour
{
    public List<ObjectBubbleTaskable> objects = new List<ObjectBubbleTaskable>(); 
    bool found = false;

    // Update is called once per frame

    void Awake(){
        objects.Add(Object.FindObjectOfType<ObjectBubbleTaskable>());
    }
    void LateUpdate()
    {
        found = false;
        foreach (ObjectBubbleTaskable obj in objects){
            if (obj != null){
                var soapBottleComponent = obj.GetComponent<SoapBottle>();
                //Debug.Log(soapBottleComponent);
                //Debug.Log((Vector3.Distance(BubbleManager.GetMousePos(), obj.transform.position)));
                if(soapBottleComponent != null && obj.neededBubbles > obj.bubbles.Count && 
                (Vector3.Distance(BubbleManager.GetMousePos(), obj.transform.position) < 2f) && 
                Input.GetMouseButtonDown(0)){
                    soapBottleComponent.AssignNewBubbleToSoapBottle();
                    Debug.Log("Happening");
                    found = true;
                }
            }
        }
        if(Input.GetMouseButtonDown(0) && !found){
            var task = new MoveTask(BubbleManager.GetMousePos());
            if(BubbleManager.playerBubbleList.Count > 0){
                BubbleManager.AssignBubble(task, BubbleManager.playerBubbleList[0]); 
            }
        }
        if(Input.GetMouseButtonDown(1)){
            Debug.Log("Recall");
            BubbleManager.RecallAllBubbles();
            return;
        }
    }
}
