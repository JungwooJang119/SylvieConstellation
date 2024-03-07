using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public float initialForce;
    Rigidbody2D rb;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        initialForce = 4f;
    }

    // Apply force to a random direction to get the stars moving
    void Start() {
        Vector2[] possibleDirections = { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1) };
        int rand = Random.Range(0, 2);
        Vector2 direction = possibleDirections[rand];
        rb.AddForce(direction * initialForce, ForceMode2D.Impulse);
    }
}
