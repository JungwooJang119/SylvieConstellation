using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealNotes : MonoBehaviour
{
    //targeting a note
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 5f;

    //random patrolling
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private bool isFollowing = false;
    [SerializeField] private GameObject current;

    void Start() {
        target.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    void Update() {
        if(!isFollowing && ChildNoteScript.correctNotes.Count != 0 && current == null) {
            target.position = ChildNoteScript.correctNotes.Dequeue().GetComponent<Transform>().position;
            isFollowing = true;
        } 
        if (isFollowing) {
            follow();
        } else {
           target.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
           isFollowing = true;
        }
    }

    void follow() {
        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        isFollowing = true;
        if(Vector2.Distance(transform.position, target.position) < 0.2f) {
           isFollowing = false;
        }
    }
    // private void random(Vector2 movement) {
    //     rb.velocity = movement * moveSpeed;
    // }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "MusicNote" && current == null) {
            col.gameObject.GetComponent<ChildNoteScript>().setGot(true);
            col.gameObject.GetComponent<ChildNoteScript>().setCorrect(false);
            col.gameObject.GetComponent<ChildNoteScript>().setSelected(false);
            current = col.gameObject;
            isFollowing = false;
        } else if (col.gameObject.tag == "Player" && current != null && col.gameObject.GetComponent<CollectNote>().currentNote == null) {
            current.gameObject.GetComponent<ChildNoteScript>().setGot(false);
            current.gameObject.GetComponent<ChildNoteScript>().setCorrect(false);
            current.gameObject.GetComponent<ChildNoteScript>().setSelected(true);
            current = null;
            isFollowing = false;
        }
            
    }
}
