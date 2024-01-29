using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : MonoBehaviour
{
    [SerializeField] private float forceMag = 10;
    [SerializeField] private float deltat = 5f;
    public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("collided!");
        if (other.gameObject.tag == "Player") {
            Rigidbody2D playerBody = other.gameObject.GetComponent<Rigidbody2D>();
            Debug.Log(playerBody.velocity);
            Vector2 forceApplied = new Vector2(-0.5f * playerBody.velocity.x * forceMag, -0.5f * playerBody.velocity.y * forceMag);
            playerBody.AddForce(forceApplied);
            //AddForceOverTime(deltat, forceApplied, playerBody);
            Debug.Log(forceApplied);
        }
    }

     public void AddForceOverTime(float time, Vector2 force, Rigidbody2D rb) {
        float timer = 0f;
        while(timer < time) {
            rb.velocity = new Vector2(rb.velocity.x - timer, rb.velocity.y - timer) * -1f * forceMag;
            timer += Time.deltaTime;
        }
    }

}