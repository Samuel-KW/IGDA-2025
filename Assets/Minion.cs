using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public BubbleTaskable task;
    private Transform player;
    // Update is called once per frame
    void Start(){
        BubbleManager.AddBubble(gameObject);
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 mouseWorldPos = BubbleManager.GetMousePos();
        //Debug.Log(mouseWorldPos);

        if(task != null){
            MoveTo(task.transform.position);
        }
        else{
            //MoveTo(mouseWorldPos);
            MoveTo(player.position);
        }
        
    }

    void MoveTo(Vector3 pos){

        var directionToPos = transform.position - pos;

        if(Vector3.Distance(transform.position, pos) < 1f){
            rb.AddForce(-directionToPos.normalized * Time.deltaTime * 10f, ForceMode2D.Impulse);
            rb.linearVelocity = Vector3.ClampMagnitude(-rb.linearVelocity, (Vector3.Distance(transform.position, pos) / 2f));
            if(rb.linearVelocity.magnitude < 0.5f){
                transform.position = transform.position;
            }
        }
        else{
            rb.AddForce(-directionToPos.normalized * Time.deltaTime * 10f, ForceMode2D.Impulse);
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, Vector3.Distance(transform.position, pos));
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Hazard")){
            BubbleManager.RemoveBubble(this.gameObject);
            Destroy(this.gameObject);
        }
    }

}
