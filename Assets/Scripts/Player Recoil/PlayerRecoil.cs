using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecoil : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;
    [SerializeField] private float forceMag = 10f;
    [SerializeField] private float deltat = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
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
    }

    public void AddForceOverTime(float time, Vector2 force) {
        float timer = 0f;
        Time.timeScale = 0.01f;
        Debug.Log(Time.timeScale);
        while(timer < time) {
            rb.AddRelativeForce(force);
            timer += Time.deltaTime;
        }
        Time.timeScale = 1f;
        Debug.Log(Time.timeScale);
    }
}
