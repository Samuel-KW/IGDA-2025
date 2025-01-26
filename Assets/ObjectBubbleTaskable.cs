using UnityEngine;
using System.Collections.Generic;

public class ObjectBubbleTaskable : MonoBehaviour
{
    [SerializeField] public int neededBubbles;
    public List<GameObject> bubbles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachBubbleToTask(GameObject bubble){
        bubbles.Add(bubble);
        Minion minion = bubble.GetComponent<Minion>();
        minion.task = this;
        //Debug.Log("Attached Bubble");
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
