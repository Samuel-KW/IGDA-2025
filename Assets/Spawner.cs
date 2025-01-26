using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int spawnCount;
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject bubbleFolder;
    [SerializeField] private int spawnTimes;
    private bool running = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 mousePos = BubbleManager.GetMousePos();

        if(Vector3.Distance(mousePos, transform.position) < 2f && spawnTimes > 0f && !running){
            spawnTimes -= 1;
            StartCoroutine("SpawnRoutine");
            running = true;
        }*/
    }

    public void Spawn(int rewardBubbles){
        spawnCount = rewardBubbles;
        StartCoroutine("SpawnRoutine");
        running = true;
    }

    IEnumerator SpawnRoutine(){
        float timeElapsed = spawnDelay;
        int tempSpawnCount = spawnCount;
        while(timeElapsed >= 0f){
            timeElapsed -= Time.deltaTime;
            if(timeElapsed <= 0f && tempSpawnCount > 0f){
                timeElapsed = spawnDelay;
                tempSpawnCount--;
                GameObject tempBubble = Instantiate(bubble, transform.position, Quaternion.identity);
                tempBubble.transform.parent = bubbleFolder.transform;
            }
            yield return null;
        }
        running = false;
        yield return null;
    }
}
