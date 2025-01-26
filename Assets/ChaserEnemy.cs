using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaserEnemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float distanceToAttack;
    private GameObject closestBubble;
    private Vector2 directionToBubble;
    [SerializeField] float speed;
    Vector3 enemyMovement;
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
            Debug.Log("was");
            if (distance < distanceToAttack && !closestBubble)
            {
                closestBubble = bubble;
                Debug.Log("will");
                
            }
        }

        if (closestBubble != null)
        {
            directionToBubble = closestBubble.transform.position - transform.position;
            
            //rb.AddForce(directionToBubble.normalized * Time.deltaTime * speed, ForceMode2D.Impulse);
            //rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, speed);
            Vector2 movement = directionToBubble.normalized * Time.deltaTime * speed;
            transform.position += new Vector3 (movement.x, movement.y, 0f);
            
            Debug.Log("is");
        }
    }


    /*void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Hazard")){
            BubbleManager.RemoveBubble(this.gameObject);
            Destroy(this.gameObject);
        }
    }*/

    
    
}
