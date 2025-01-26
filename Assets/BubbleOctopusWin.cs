using UnityEngine;

public class BubbleOctopusWin : ObjectBubbleTaskable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bubbles.Count >= neededBubbles){
            BubbleManager.Win();
        }
    }
}
