using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSwap : IState
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private GameObject swap1;
    [SerializeField] private GameObject swap2;
    [SerializeField] private Transform transform;
    [SerializeField] private Vector2 slot1;
    [SerializeField] private Vector2 slot2;
    [SerializeField] private bool finished;
    [SerializeField] private bool swapped1;
    [SerializeField] private bool swapped2;
    [SerializeField] private IState next;

    public StateSwap(Transform t) {
        transform = t;
        finished = false;
        swapped1 = false;
        swapped2 = false;
    }
    public void setNext(IState n) {
        next = n;
    }
    public void Enter() {
        Debug.Log("starting swap");
        finished = false;
        //Debug.Log(StealNotes.hasStolenNote());
        swap1 = StealNotes.getStolenNote();
        //Debug.Log(swap1 == null);
        swap2 = ChildNoteScript.correctNotes[0];
        //Debug.Log(swap2 == null);
        slot1 = new Vector2(swap1.transform.position.x, swap1.transform.position.y);
        //Debug.Log(slot1.x + ", " + slot1.y);
        slot2 = new Vector2(swap2.transform.position.x, swap2.transform.position.y);
        //Debug.Log(slot2.x + ", " + slot2.y);
    }

    public void Execute() {
        chaseSwap();
        if (swapped1 && swapped2) {
            finished = true;
        }

    }
    void chaseSwap() {
        if (!swapped1) {
            transform.position = Vector2.MoveTowards(transform.position, slot2, Time.deltaTime * moveSpeed);
        } else if (!swapped2) {
            transform.position = Vector2.MoveTowards(transform.position, slot1, Time.deltaTime * moveSpeed);
            if (Vector2.Distance(transform.position, slot1) <= 1f) {
                swapped2 = true;
                swap2.transform.position = new Vector3(slot1.x, slot1.y, 0);
                StealNotes.getStolenNote().GetComponent<ChildNoteScript>().setGot(false);
                
            }
        }
    }

    public void Exit() {
        Debug.Log("ending swap");
        StealNotes.setStolenNote(null);
        swap1 = null;
        swap2 = null;
        swapped1 = false;
        swapped2 = false;
        finished = true;
    }
    public bool Finished() {
        return finished;
    }
    public IState getNext() {
        return next;
    }
    public GameObject getSwap1() {
        return swap1;
    }
    public GameObject getSwap2() {
        return swap2;
    }
    public void setSwapped1(bool b) {
        swapped1 = b;
    }
    public void setSwapped2(bool b) {
        swapped2 = b;
    }
}
