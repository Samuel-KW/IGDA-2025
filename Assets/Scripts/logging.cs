using UnityEngine;

public class logging : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector2 myInput;
    [SerializeField] GameObject square;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myInput.y = Input.GetAxisRaw("Horizontal"); //A D Keys
        myInput.x = Input.GetAxisRaw("Vertical"); //W S Keys
        Debug.DrawRay(transform.position, myInput.normalized, Color.white, 1f);
        Debug.Log(myInput);
        GameObject temporarySquare = Instantiate(square, (Vector2)transform.position + myInput, new Quaternion(0, 0, 0, 0));
        if (temporarySquare != null)
        {
            Destroy(temporarySquare, 1f);
        }
    }
}
