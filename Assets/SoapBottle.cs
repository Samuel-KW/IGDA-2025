using UnityEngine;
using System.Collections.Generic;

public class SoapBottle : ObjectBubbleTaskable
{
    [SerializeField] int rewardBubbles;
    [SerializeField] Spawner spawner;

    [SerializeField] float speed;
    private bool proximity = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject bubble in bubbles){
            if(!(Vector3.Distance(bubble.transform.position, transform.position) < 3f)){
                proximity = false;
                return;
            }
            proximity = true;
        }
        if(neededBubbles <= bubbles.Count){
            PushObjectToNearestSpawner();
        }
    }

    public void AssignNewBubbleToSoapBottle(){
        if(BubbleManager.playerBubbleList.Count > 0){
            GameObject bubbleToAssign = BubbleManager.playerBubbleList[0];
            BubbleManager.AssignBubble(this, bubbleToAssign);
        }
    }

    void PushObjectToNearestSpawner(){
        Vector3 moveDirection = spawner.transform.position - transform.position;
        Vector3 movement = moveDirection.normalized * Time.deltaTime * speed;
        transform.position += movement;
        if(Vector3.Distance(transform.position, spawner.transform.position) < 1f){
            DetachBubblesFromTask();
            spawner.Spawn(rewardBubbles);
            Destroy(gameObject);
        }
    }

    
}
