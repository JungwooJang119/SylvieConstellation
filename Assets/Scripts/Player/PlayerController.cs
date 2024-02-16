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
    private Vector3 lastPosition;

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
        lastPosition = transform.position;
        SetBGOffset();
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

            float movementX = CalculateMovement(inputVector.x, rb.velocity.x);
            float movementY = CalculateMovement(inputVector.y, rb.velocity.y);
            rb.AddForce(movementX * Vector2.right);
            rb.AddForce(movementY * Vector2.up);

            if (inputVector.magnitude > 0) {
                Vector2 normMovement = inputVector.normalized;
                anim.SetBool("isMoving", true);
                if (isBoosted) {
                    //Play Boost Star Particles
                }
            } else {
                anim.SetBool("isMoving", false);
            }
        }
    }

    void Update() {
        
        var currentPosition = transform.position;
        if (currentPosition != lastPosition)
        {
            SetBGOffset();
        }
        lastPosition = currentPosition;
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
    
    private float CalculateMovement(float value, float velocityVal) {
        float targetSpeed = value * speed;
        float speedDiff = targetSpeed - velocityVal;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);
        return movement;
    }

    private void SetBGOffset() {
        //BG stars offset
        //Close stars
        bgCloseOffset.x = transform.position.x / transform.localScale.x / parallax;
        bgCloseOffset.y = transform.position.y / transform.localScale.y / parallax;
        bgClose.SetVector("_Offset", new Vector2(bgCloseOffset.x, bgCloseOffset.y));

        //Far stars
        bgFarOffset.x = transform.position.x / transform.localScale.x / (parallax * 10);
        bgFarOffset.y = transform.position.y / transform.localScale.y / (parallax * 10);
        bgFar.SetVector("_Offset", new Vector2(bgFarOffset.x, bgFarOffset.y));
    }
   
}
