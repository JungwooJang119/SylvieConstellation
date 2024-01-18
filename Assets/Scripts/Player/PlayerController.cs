using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float speed = 7f;

    public bool canMove = true;

    private Vector2 inputVector;
    private Rigidbody2D rb;

    private PlayerInput input;
    private InputAction movement;

    private Animator anim;

    public float acceleration;
    public float decceleration;
    public float velPower;

    [SerializeField] private Material bgClose;
    [SerializeField] private Material bgFar;
    [SerializeField] private float parallax;

    private Vector2 bgCloseOffset;
    private Vector2 bgFarOffset;

    [SerializeField] private float boostTime;
    private float holdSpeed;
    public float currTime;
    private bool isBoosted;
    public ParticleSystem starBits;

    private void Awake() {
        InitializeSingleton();
        input = new PlayerInput();
        input.Player.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canMove = true;
        holdSpeed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isBoosted) {
            currTime += Time.deltaTime;
        }
        if (currTime >= boostTime) {
            speed = holdSpeed;
            isBoosted = false;
        }
        if (canMove) {
            inputVector = input.Player.Move.ReadValue<Vector2>();
            anim.SetFloat("X", inputVector.x);
            anim.SetFloat("Y", inputVector.y);
            float targetSpeedX = inputVector.x * speed;
            float targetSpeedY = inputVector.y * speed;
            float speedDiffX = targetSpeedX - rb.velocity.x;
            float speedDiffY = targetSpeedY - rb.velocity.y;
            float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? acceleration : decceleration;
            float accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? acceleration : decceleration;
            float movementX = Mathf.Pow(Mathf.Abs(speedDiffX) * accelRateX, velPower) * Mathf.Sign(speedDiffX);
            float movementY = Mathf.Pow(Mathf.Abs(speedDiffY) * accelRateY, velPower) * Mathf.Sign(speedDiffY);
            rb.AddForce(movementX * Vector2.right);
            rb.AddForce(movementY * Vector2.up);
            bgCloseOffset.x = transform.position.x / transform.localScale.x / parallax;
            bgCloseOffset.y = transform.position.y / transform.localScale.y / parallax;
            bgClose.SetVector("_Offset", new Vector2(bgCloseOffset.x, bgCloseOffset.y));
            bgFarOffset.x = transform.position.x / transform.localScale.x / (parallax * 10);
            bgFarOffset.y = transform.position.y / transform.localScale.y / (parallax * 10);
            bgFar.SetVector("_Offset", new Vector2(bgFarOffset.x, bgFarOffset.y));
            if (inputVector.magnitude > 0) {
                Vector2 normMovement = inputVector.normalized;
                anim.SetBool("isMoving", true);
                if (isBoosted) {
                    starBits.Play();
                }
            } else {
                anim.SetBool("isMoving", false);
            }
        }
    }

    private void OnMove(InputValue movementValue) {
    }

    private void OnBoost() {
        isBoosted = true;
        currTime = 0;
        speed = holdSpeed * 3;
    }

    private void OnEnable() {
        movement = input.Player.Move;
        movement.Enable();
    }

    private void OnDisable() {
        movement = input.Player.Move;
        movement.Disable();
    }
   
}
