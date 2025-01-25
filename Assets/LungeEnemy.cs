using UnityEngine;

public class LungeEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float distanceToAttack = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float chargedTime = 2f; // Time spent "charging" before lunging
    [SerializeField] private float lungedTime = 1f;  // Time spent in "lunged" state

    private GameObject closestBubble = null;
    private Vector2 directionToBubble;
    private float chargedTimer = 0f;
    private float lungedTimer = 0f;

    void Update()
    {
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
                // Prepare to lunge towards the bubble
                lungedTimer = lungedTime;
                directionToBubble = closestBubble.transform.position - transform.position;

                // Lunge toward the bubble using Rigidbody2D physics
                rb.velocity = directionToBubble.normalized * speed;
            }
        }
        else if (lungedTimer <= 0f)
        {
            // Stop moving after lunging
            rb.velocity = Vector2.zero;
        }
    }

    private GameObject FindClosestBubble()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject bubble in BubbleManager.playerBubbleList)
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
}
