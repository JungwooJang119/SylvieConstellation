using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecoil : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;
    [SerializeField] private float forceMag = 10f;
    [SerializeField] private float deltat = 5f;
    [SerializeField] private float rotation = 0.005f;
    [SerializeField] private GameObject centerPosition;
    private Vector3 positionAfterCollision;

    private PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        centerPosition = this.transform.GetChild(0).gameObject;
        pc = this.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       velocity = rb.velocity;
    }

    public void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("Collision detected!");
        Debug.Log("Player velocity: " + velocity);
        Vector2 direction = -1f * velocity.normalized;
        Debug.Log("direction: " + direction);
        rb.velocity = velocity;
        //rb.AddForce(forceMag * direction);
        AddForceOverTime(deltat, forceMag * direction);
        Invoke("StopRotating", deltat * 0.75f);
    }

    private void AddForceOverTime(float time, Vector2 force) {
        float timer = 0f;
        //Debug.Log(Time.timeScale);
        pc.canMove = false;
        rb.freezeRotation = false;
        while(timer < time) {
            rb.AddRelativeForce(force);
            rb.AddTorque(rotation, ForceMode2D.Force);
            //rb.angularVelocity = -5f;
            timer += Time.deltaTime;
        }
        timer = 0f;
        // while(timer < (time * 0.5f)) {
        //     rb.AddTorque(-rotation * 0.5f, ForceMode2D.Force);
        //     timer += Time.deltaTime;
        // }
        //positionAfterCollision = centerPosition.gameObject.transform.position;
        //rb.freezeRotation = true;
        
        //Debug.Log(Time.timeScale);
    }

    private void StopRotating() {
        Debug.Log("Player position: " + rb.gameObject.transform.position);
        rb.gameObject.transform.rotation = Quaternion.identity;
        rb.gameObject.transform.position = centerPosition.gameObject.transform.position;
        Debug.Log("Player position: " + rb.gameObject.transform.position);
        rb.freezeRotation = true;
        pc.canMove = true;
    }
}
