using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleOctopusWin : ObjectBubbleTaskable
{

    public bool lastLevel = false;
    public string nextLevel = string.Empty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!lastLevel && nextLevel.Length == 0)
        {
            Debug.LogError("You must specify the next level");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(bubbles.Count >= neededBubbles)
        {
            if (lastLevel)
            {
                BubbleManager.Win();
                return;
            }
            else
            {
                Debug.Log("Next level: " + nextLevel);
                SceneManager.LoadScene(nextLevel);
                BubbleManager.RemoveAllBubbles();
            }
        } 
    }
}
