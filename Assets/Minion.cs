using Microsoft.CSharp;
using UnityEngine;
using System.Dynamic;
using System.Collections;

public class Minion : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public dynamic task;
    private Transform player;
    // Update is called once per frame
    void Start(){
        BubbleManager.AddBubble(gameObject);
        Debug.Log("Start Coroutine");
        StartCoroutine(GetPlayer());
    }

    void Update()
    {
        Vector3 mouseWorldPos = BubbleManager.GetMousePos();
        //Debug.Log(mouseWorldPos);

        //Debug.Log(typeof(ObjectBubbleTaskable));
        /*if(task != null){
            Debug.Log("Physical task: " + typeof(ObjectBubbleTaskable).IsAssignableFrom(task.GetType()));
            Debug.Log("Meta task: " + typeof(BubbleTaskable).IsAssignableFrom(task.GetType()));
        }*/

        if(task != null && typeof(ObjectBubbleTaskable).IsAssignableFrom(task.GetType())){
            //Debug.Log("Object Bubble Task");
            MoveTo(task.transform.position);
        }
        else if(task != null && typeof(BubbleTaskable).IsAssignableFrom(task.GetType())){
            //Debug.Log("Meta Bubble Task");
            MoveTo(task.movePosition);
        }
        else if(player != null){
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


    IEnumerator GetPlayer() {
        Debug.Log("CoRoutine Started");
        float timeWaited = 0f;
        while (timeWaited < 5f) {  // Try for 5 seconds max
            Debug.Log("Executing");
            
            player = GameObject.Find("Player")?.transform;
            if (player != null) {

                Debug.Log(player);
                break;  // Exit if found
            }
            timeWaited += Time.deltaTime;
            Debug.Log(timeWaited);
            yield return null;  // Wait until next frame
        }

        Debug.LogWarning("Player not found after waiting.");

        if (player == null) {
            Debug.LogWarning("Player not found after waiting.");
        }
    }



}
