using UnityEngine;
using System.Collections.Generic;

public class Cookie : BubbleTaskable
{
    [SerializeField] int rewardBubbles;
    [SerializeField] Spawner spawner;

    [SerializeField] float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if(neededBubbles > bubbles.Count && Vector3.Distance(BubbleManager.GetMousePos(), transform.position) < 2f && Input.GetMouseButtonDown(0)){
            Debug.Log("Has Bubbles");
            if(BubbleManager.allBubbleList.Count > 0){
                BubbleManager.AssignBubble(this, BubbleManager.allBubbleList[0]);
            }
        }
        if(neededBubbles <= bubbles.Count){
            PushObjectToNearestSpawner();
        }

    }

    void PushObjectToNearestSpawner(){
        Vector3 moveDirection = spawner.transform.position - transform.position;
        Vector3 movement = moveDirection.normalized * Time.deltaTime * speed;
        transform.position += movement;
        if(Vector3.Distance(transform.position, spawner.transform.position) < 1f){
            spawner.Spawn(rewardBubbles);
            Destroy(gameObject);
        }
    }

    
}
