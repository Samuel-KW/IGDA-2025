using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    // Update is called once per frame
    void Start(){
        BubbleManager.AddBubble(this.gameObject);
    }

    void Update()
    {
        Vector3 mouseWorldPos = BubbleManager.GetMousePos();
        //Debug.Log(mouseWorldPos);

        var directionToMouse = transform.position - mouseWorldPos;

        if(Vector3.Distance(transform.position, mouseWorldPos) < 1f){
            rb.AddForce(-directionToMouse.normalized * Time.deltaTime * 10f, ForceMode2D.Impulse);
            rb.linearVelocity = Vector3.ClampMagnitude(-rb.linearVelocity, (Vector3.Distance(transform.position, mouseWorldPos) / 2f));
            if(rb.linearVelocity.magnitude < 0.5f){
                transform.position = transform.position;
            }
        }
        else{
            rb.AddForce(-directionToMouse.normalized * Time.deltaTime * 10f, ForceMode2D.Impulse);
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, Vector3.Distance(transform.position, mouseWorldPos));
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Hazard")){
            BubbleManager.RemoveBubble(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
