using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecoil : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;
    [SerializeField] private float forceMag = 8f;
    [SerializeField] private float deltat = 10f;
    [SerializeField] private float rotation = 0.03f;
    [SerializeField] private GameObject centerPosition;
    private Vector3 positionAfterCollision;

    private PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        //getting components
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        centerPosition = this.transform.GetChild(0).gameObject;
        pc = this.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //saving the velocity right before collision
        //necessary because velocity at collision == 0
       velocity = rb.velocity;
    }

    //when slyvie collides with anything
    public void OnCollisionEnter2D(Collision2D col) {

        //store the opposite direction
        Vector2 direction = -1f * velocity.normalized;
        
        //set her current velocity to her last velocity 
        //prevents AddForce on a 0 vector
        rb.velocity = velocity;

        //add the force
        AddForceOverTime(deltat, forceMag * direction * velocity.magnitude * 0.5f);

        //cause her to stop rotating after a specific amount of time
        // Invoke("StopRotating", deltat * 0.5f);
    }

    //this method adds the force to Sylvie
    private void AddForceOverTime(float time, Vector2 force) {
        float timer = 0f;
        //unfreeze rotation so that she can spin
        //rb.freezeRotation = false;

        //add the initial spin force
        //rb.AddTorque(rotation * velocity.magnitude * 0.5f, ForceMode2D.Impulse);

        //for a specific duration of time
        while(timer < time) {
            //add the force
            rb.AddRelativeForce(force);
            timer += Time.deltaTime;
        }
    }

    //stops her rotation
    private void StopRotating() {
        //reset her rotation to 0
        rb.gameObject.transform.rotation = Quaternion.identity;

        //REMOVE WHEN HER SPRITE IS FINALIZED!
        rb.gameObject.transform.position = centerPosition.gameObject.transform.position;

        //refreeze her rotation so that she doesn't spin constantly
        rb.freezeRotation = true;
    }
}
