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
        objects.AddRange(Resources.FindObjectsOfTypeAll(typeof(ObjectBubbleTaskable)) as ObjectBubbleTaskable[]);
    }
    void LateUpdate()
    {
        found = false;
        foreach (ObjectBubbleTaskable obj in objects){
            if (obj != null){
                var component = obj.GetComponent<ObjectBubbleTaskable>();
                if(obj.GetComponent<SoapBottle>() != null){
                    component = obj.GetComponent<SoapBottle>();
                }
                if(obj.GetComponent<BubbleOctopusWin>() != null){
                    component = obj.GetComponent<BubbleOctopusWin>();
                }
                
                
                //Debug.Log(soapBottleComponent);
                //Debug.Log((Vector3.Distance(BubbleManager.GetMousePos(), obj.transform.position)));
                if(component != null && obj.neededBubbles > obj.bubbles.Count && 
                (Vector3.Distance(BubbleManager.GetMousePos(), obj.transform.position) < 2f) && 
                Input.GetMouseButtonDown(0)){
                    component.AssignNewBubble();
                    //Debug.Log("Happening");
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
