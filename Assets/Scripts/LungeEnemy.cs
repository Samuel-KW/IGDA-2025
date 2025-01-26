using UnityEngine;

public class LungeEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float distanceToAttack = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float chargedTime = 2f; // Time spent "charging" before lunging
    [SerializeField] private float lungedTime = 1f;  // Time spent in "lunged" state
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer spriteRenderer;

    private GameObject closestBubble = null;
    private Vector2 directionToBubble;
    private float chargedTimer = 0f;
    private float lungedTimer = 0f;

    void Update()
    {
        closestBubble = FindClosestBubble();
        if(closestBubble == null){
            lungedTimer = lungedTime;
            chargedTimer = chargedTime;
        }
        // Handle timers
        if (lungedTimer > 0f)
        {
            lungedTimer -= Time.deltaTime;
            if (lungedTimer <= 0f)
            {
                // After lunging, reset to charging state
                chargedTimer = chargedTime;
                closestBubble = null; // Reset target
            }
        }
        else if (chargedTimer > 0f)
        {
            chargedTimer -= Time.deltaTime;
        }

        // Find the closest bubble within range
        if (lungedTimer <= 0f && chargedTimer <= 0f)
        {
            closestBubble = FindClosestBubble();
            if (closestBubble != null)
            {
                spriteRenderer.sprite = sprites[0];
                // Prepare to lunge towards the bubble
                lungedTimer = lungedTime;
                directionToBubble = closestBubble.transform.position - transform.position;

                // Lunge toward the bubble using Rigidbody2D physics
                rb.AddForce(-transform.right * speed, ForceMode2D.Impulse);
                chargedTimer = 0f;
                rb.angularVelocity = 0f;
            }
        }
        else if (lungedTimer <= 0f)
        {
            // Stop moving after lunging
            spriteRenderer.sprite = sprites[1];
            rb.linearVelocity = Vector2.zero;

            closestBubble = FindClosestBubble();
            if(closestBubble != null){
                //Debug.Log(closestBubble);
                Vector3 targ = closestBubble.transform.position;
                targ.z = 0f;

                Vector3 objectPos = transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, (angle-180)));
            }
        }
    }

    private GameObject FindClosestBubble()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject bubble in BubbleManager.allBubbleList)
        {
            float distance = Vector2.Distance(transform.position, bubble.transform.position);
            if (distance < distanceToAttack && distance < minDistance)
            {
                closest = bubble;
                minDistance = distance;
            }
        }

        return closest;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Hazard")){
            Debug.Log("Killed Enemy");
            Destroy(this.gameObject);
        }
    }
    
}
