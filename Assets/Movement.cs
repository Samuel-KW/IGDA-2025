using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour
{
    Vector3 myVector;
    float speed = 5;
    Rigidbody2D rb;
    [SerializeField] float jumpAmount;
    [SerializeField] float jumpDelay;
    float elapsedTime = 1f;
    private int currentSpriteIndex = 0;
    [SerializeField] private float frameRate;
    [SerializeField] private Sprite jumpSprite;

    private bool running = false;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite floatSprite;


    bool isGrounded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        myVector.x = Input.GetAxisRaw("Horizontal");
        myVector.y = Input.GetAxisRaw("Vertical");
        if (isGrounded)
        {
            if (myVector.x != 0)
            {
                if (!running)
                {
                    StartCoroutine("AnimateSprites");
                }
                if(myVector.x > 0) {
                    spriteRenderer.flipX = false ;
                }
                else if (myVector.x < 0)
                {
                    spriteRenderer.flipX = true;   
                }
            }
            //
            else
            {
                running = false;
                StopCoroutine("AnimateSprites");
                spriteRenderer.sprite = sprites[0];  
            }
            //
            rb.linearVelocity = new Vector2(myVector.x * speed, rb.linearVelocity.y);
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)

            {
                spriteRenderer.sprite = jumpSprite;
                rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
                isGrounded = false;
                elapsedTime = jumpDelay;
                
            }
        }
        else if(Input.GetKey(KeyCode.Space) && elapsedTime <= 0f)
        {
            spriteRenderer.sprite = floatSprite;
            running = false;
            StopCoroutine("AnimateSprites");
       
            rb.linearVelocity = new Vector2(myVector.x * speed, myVector.y * speed);
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, speed);
        }
        
        elapsedTime -= Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.collider.gameObject.tag);
        if (col.collider.gameObject.CompareTag("Ground"))
        {
            
            var velocity = rb.linearVelocity;
            isGrounded = true;
            rb.linearVelocity = new Vector2(velocity.x, 0);
            spriteRenderer.sprite = sprites[0];
        }
    }

    private IEnumerator AnimateSprites()
    {
        running = true;
        while (running)
        {
            if (isGrounded)
            {
                spriteRenderer.sprite = sprites[currentSpriteIndex];
                currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
                yield return new WaitForSeconds(frameRate);
            }
            else
            {
                spriteRenderer.sprite = sprites[currentSpriteIndex];
                yield return new WaitForSeconds(frameRate);
            }
            
        }
    }




}
