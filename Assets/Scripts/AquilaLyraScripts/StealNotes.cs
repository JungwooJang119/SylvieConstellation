using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealNotes : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float accelerationTime = 3f;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movement;
    [SerializeField] private float timeLeft = 0f;
    private bool isFollowing = false;
    private GameObject current = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (ChildNoteScript.correctNotes.Count != 0) {
            follow();
        } else if (!isFollowing) {
            if(timeLeft <= 0)
            {
                movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                timeLeft += Random.Range(1, accelerationTime);
            }
            random(movement);
        }
    }

    private void follow() {
        if (!isFollowing) {
            current = ChildNoteScript.correctNotes.Dequeue();
        }
        transform.position = Vector3.MoveTowards(transform.position, current.transform.position, moveSpeed);
        isFollowing = true;
    }
    private void random(Vector2 movement) {
        rb.velocity = movement * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (!col.gameObject.name.Contains("note")) {
            return;
        }
        if (isFollowing && col.gameObject == current) {
            col.gameObject.GetComponent<ChildNoteScript>().setGot(true);
            col.gameObject.GetComponent<ChildNoteScript>().setCorrect(false);
            isFollowing = false;
        } else if (current == null) {
            current = col.gameObject;
            col.gameObject.GetComponent<ChildNoteScript>().setGot(true);
            col.gameObject.GetComponent<ChildNoteScript>().setCorrect(false);
            isFollowing = false;
        }
    }
}
