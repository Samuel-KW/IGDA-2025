using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class IntroWindows : MonoBehaviour
{

    public GameObject cameraObject; // The camera to move

    public GameObject targetObject; // The object to move
    public float moveSpeed = 2f; // Speed at which the object moves up
    public float returnSpeed = 1f; // Speed at which the object returns to its original position
    public float maxHeight = 5f; // Maximum height above the original position

    private Vector3 originalPosition; // Original position of the target object
    private bool isHovering = false; // Whether the mouse is hovering over the trigger
    private bool loadLevel = false; // Whether the game has started (window clicked)

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object is not assigned");
            return;
        }

        originalPosition = targetObject.transform.position;
    }

    void OnMouseOver()
    {
        isHovering = true;
    }

    void OnMouseExit()
    {
        isHovering = false;
    }

    void Update()
    {
        if (isHovering || loadLevel)
        {
            // Move the target object up until it reaches the max height
            Vector3 targetPosition = new Vector3(
                originalPosition.x,
                Mathf.Min(originalPosition.y + maxHeight, targetObject.transform.position.y + moveSpeed * Time.deltaTime),
                originalPosition.z
            );

            targetObject.transform.position = targetPosition;

            // Handle clicking window
            if (Input.GetMouseButtonDown(0) || loadLevel)
            {
                if (!loadLevel)
                {
                    StartCoroutine(LoadAsyncScene());
                    loadLevel = true;
                }

                Vector3 lookDirection = targetObject.transform.position - cameraObject.transform.position;
                lookDirection.Normalize();

                cameraObject.transform.rotation = Quaternion.Slerp(cameraObject.transform.rotation, Quaternion.LookRotation(lookDirection), 1 * Time.deltaTime);
            }
        }
        else
        {
            // Return the target object to its original position
            targetObject.transform.position = Vector3.MoveTowards(
                targetObject.transform.position,
                originalPosition,
                returnSpeed * Time.deltaTime
            );
        }
    }

    IEnumerator LoadAsyncScene()
    {

        // Loads the Scene in the background as the current Scene runs.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/level1");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
