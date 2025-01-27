using UnityEngine;

public class ChaserEnemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float distanceToAttack;
    private GameObject closestBubble;
    private Vector2 directionToBubble;
    [SerializeField] float speed;
    Vector3 enemyMovement;

    float strength = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        foreach (GameObject bubble in BubbleManager.playerBubbleList)
        {
            float distance = Vector2.Distance(transform.position, bubble.transform.position);
            //Debug.Log("was");
            if (distance < distanceToAttack && !closestBubble)
            {
                closestBubble = bubble;
                //Debug.Log("will");
                
            }
        }

        if (closestBubble != null)
        {
            directionToBubble = closestBubble.transform.position - transform.position;
            
            //rb.AddForce(directionToBubble.normalized * Time.deltaTime * speed, ForceMode2D.Impulse);
            //rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, speed);
            Vector2 movement = directionToBubble.normalized * Time.deltaTime * speed;
            transform.position += new Vector3 (movement.x, movement.y, 0f);
            
            //Debug.Log("is");
            Vector3 targ = closestBubble.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }
        else{
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }


    void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Hazard")){
            BubbleManager.RemoveBubble(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    
    
}
