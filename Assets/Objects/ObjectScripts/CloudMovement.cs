using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f; // Speed at which the clouds move
    public float fadeDuration = 2f; // Duration of fade-in and fade-out
    public float lifeTime = 20f; // Time clouds stay visible before fading out
    public Vector3 startPosition; // Starting position for the clouds
    public Vector3 endPosition; // End position for the clouds
    
    private Renderer cloudRenderer;
    private float fadeTimer;
    private bool isFadingIn;
    private bool isFadingOut;
    private float lifeTimer;

    void Start()
    {
        cloudRenderer = GetComponent<Renderer>();
        if (cloudRenderer == null)
        {
            Debug.LogError("Cloud Renderer not found");
            return;
        }

        ResetCloud();
    }

    void Update()
    {
        if (cloudRenderer == null) return;

        // Move the cloud
        transform.position = Vector3.Lerp(startPosition, endPosition, lifeTimer / lifeTime);
        lifeTimer += Time.deltaTime;

        // Handle fading
        if (isFadingIn)
        {
            fadeTimer += Time.deltaTime;
            SetCloudOpacity(fadeTimer / fadeDuration);

            if (fadeTimer >= fadeDuration)
            {
                isFadingIn = false;
                fadeTimer = 0;
            }
        }
        else if (isFadingOut)
        {
            fadeTimer += Time.deltaTime;
            SetCloudOpacity(1 - (fadeTimer / fadeDuration));

            if (fadeTimer >= fadeDuration)
            {
                ResetCloud();
            }
        }

        // Trigger fade-out when the cloud's lifetime ends
        if (lifeTimer >= lifeTime && !isFadingOut)
        {
            StartFadeOut();
        }
    }

    void ResetCloud()
    {
        transform.position = startPosition;
        lifeTimer = 0;
        fadeTimer = 0;
        isFadingIn = true;
        isFadingOut = false;
        SetCloudOpacity(0); // Start fully transparent
    }
    
    void StartFadeOut()
    {
        isFadingOut = true;
        fadeTimer = 0;
    }

    void SetCloudOpacity(float alpha)
    {
        if (cloudRenderer.material.HasProperty("_Color"))
        {
            Color color = cloudRenderer.material.color;
            color.a = Mathf.Clamp01(alpha);
            cloudRenderer.material.color = color;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the start and end positions in the editor
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(startPosition, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPosition, 0.5f);
    }
}

