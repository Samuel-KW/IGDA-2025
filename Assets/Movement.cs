using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 myVector;
    float speed = 5;
    Rigidbody2D rb;
    [SerializeField] float jumpAmount;
    [SerializeField] float jumpDelay;
    float elapsedTime = 0f;


    bool isGrounded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myVector.x = Input.GetAxisRaw("Horizontal");
        myVector.y = Input.GetAxisRaw("Vertical");
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(myVector.x * speed, rb.linearVelocity.y);
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)

            {
             
                rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
                isGrounded = false;
                elapsedTime = jumpDelay;
                
            }
        }
        else if(Input.GetKey(KeyCode.Space) && elapsedTime <= 0f)
        {
          
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
            
        }
    }




}
