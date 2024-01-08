using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float speed = 7f;

    public bool canMove = true;

    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake() {
        InitializeSingleton(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove) {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    private void OnMove(InputValue movementValue) {
        movement = movementValue.Get<Vector2>();
        print("yes");
    }
}
