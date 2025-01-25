using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LungeEnemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float distanceToAttack;
    private GameObject closestBubble;
    private Vector2 directionToBubble;
    [SerializeField] float speed;
    private float chargedTimer;
    private float lungedTimer;
    [SerializeField] float chargedTime;
    [SerializeField] float lungedTime;
    Vector3 enemyMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lungedTimer > 0f)
        
        chargedTimer -= Time.deltaTime;
        Debug.Log("Charge" + chargedTimer);
        lungedTimer -= Time.deltaTime;
        Debug.Log("Lunge" + lungedTimer);

        foreach (GameObject bubble in BubbleManager.playerBubbleList)
        {
            float distance = Vector2.Distance(transform.position, bubble.transform.position);
            Debug.Log("was");
            if (distance < distanceToAttack && !closestBubble)
            {
                closestBubble = bubble;
                Debug.Log("will");
                
            }
        }
        Debug.Log("Good");
        if (closestBubble != null && chargedTimer <= 0f)
        {
            lungedTimer = lungedTime;
            directionToBubble = closestBubble.transform.position - transform.position;
            //rb.AddForce(directionToBubble.normalized * Time.deltaTime * speed, ForceMode2D.Impulse);
            //rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, speed);
            Vector2 movement = directionToBubble.normalized * Time.deltaTime * speed;
            transform.position += new Vector3 (movement.x, movement.y, 0f);
            
            Debug.Log("is");
        }
        Debug.Log("Gooder");
        if (lungedTimer <= 0f && closestBubble != null)
        {
            closestBubble = null;
            chargedTimer = chargedTime;
        }
    }


    /*void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Hazard")){
            BubbleManager.RemoveBubble(this.gameObject);
            Destroy(this.gameObject);
        }
    }*/

    
    
}
