using UnityEngine;
using System.Collections;
public class PlayerAnimation : MonoBehaviour
{
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private Sprite floatSprite;
    private int currentSpriteIndex = 0;
    private float frameRate = 1f;
    bool isGrounded = true;
    bool isRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("art/childRunAnimation");
        floatSprite = Resources.Load<Sprite>("art/childFloat");
        /*
        if (sprites.Length > 0)
        {
            StartCoroutine(AnimateSprites());
            isRunning = true;
        }
        */
    }
    void Update()
    {
        /*
        if (GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        
        if (!isGrounded)
        {
            spriteRenderer.sprite = floatSprite;
            StopCoroutine(AnimateSprites());  
        }
        */
        if (GetComponent<Rigidbody2D>().linearVelocity.y == 0)
        {
            if (!isGrounded)
            {
                isGrounded = true;
                if (!isRunning && sprites.Length > 0)
                {
                    StartCoroutine(AnimateSprites());
                    isRunning = true;
                }
            }
        }
        else
        {
            if (isGrounded)
            {
                isGrounded = false;
                spriteRenderer.sprite = floatSprite;
                StopCoroutine(AnimateSprites());
                isRunning = false;
            }
        }
    }
            // Update is called once per frame

            private IEnumerator AnimateSprites()
        {
            while (true)
            {
                spriteRenderer.sprite = sprites[currentSpriteIndex];  
                currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;  
                yield return new WaitForSeconds(frameRate); 
            }
        }
    
}
