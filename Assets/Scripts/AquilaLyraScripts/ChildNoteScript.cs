using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildNoteScript : MonoBehaviour
{
    [SerializeField] private bool isCorrect = false;
    [SerializeField] private bool isSelected = false;
    [SerializeField] private bool isGot = false;
    [SerializeField] private Transform player;
    [SerializeField] private Transform aquilla;
    [SerializeField] private Rigidbody2D rb;

    public static Queue<GameObject> correctNotes;

    public bool getCorrect() {
        return isCorrect;
    }

    public void setCorrect(bool b) {
        isCorrect = b;
    }

    public bool getSelected() {
        return isSelected;
    }

    public void setSelected(bool b) {
        isSelected = b;
    }

    public bool getGot() {
        return isGot;
    }

    public void setGot(bool b) {
        isGot = b;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        correctNotes = new Queue<GameObject>();
    }
    void FixedUpdate()
    {
        if (isCorrect) {

        } else if (isSelected && !isGot) {
            follow(player);
        } else if (isGot) {
            follow(aquilla);
        } 
    }
    public void follow(Transform target) {
        transform.position = target.position;
    }

    
}
