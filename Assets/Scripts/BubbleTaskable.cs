using UnityEngine;
using System.Collections.Generic;

public class BubbleTaskable
{
    public Vector3 movePosition;
    public List<GameObject> bubbles = new List<GameObject>();
    public BubbleTaskable(Vector3 pos)
    {
        movePosition = pos;
    }


    public void AttachBubbleToTask(GameObject bubble){
        //Debug.Log("Attached Bubble");
        bubbles.Add(bubble);
        Minion minion = bubble.GetComponent<Minion>();
        minion.task = this;
    }

    public void DetachBubblesFromTask(){
        foreach(GameObject bubble in bubbles){
            BubbleManager.AddBubble(bubble);
            Minion minion = bubble.GetComponent<Minion>();
            minion.task = null;
        }
        bubbles.Clear();
    }
}
