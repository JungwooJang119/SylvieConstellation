using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Comes after steal
* swaps the oldest 2 notes that are in their correct spots
*/
public class StateSwap : IState
{
    
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private GameObject swap1; //note 1
    [SerializeField] private GameObject swap2; //note 2
    [SerializeField] private Transform transform; //aquilla's position
    [SerializeField] private Vector2 slot1; //where note 2 needs to get moved to
    [SerializeField] private Vector2 slot2; //where note 1 needs to get moved to
    [SerializeField] private bool finished; //are both  notes placed or did we lose a note
    [SerializeField] private bool swapped1; //whether or not we placed note 1
    [SerializeField] private bool swapped2; //whether or not we placed note 2
    [SerializeField] private IState next; //idle

    public StateSwap(Transform t) {
        transform = t;
        finished = false;
        swapped1 = false;
        swapped2 = false;
    }
    public void setNext(IState n) {
        next = n;
    }
    
    //initialize what notes are going where
    public void Enter() {
        finished = false;
        swap1 = StealNotes.getStolenNote();
        swap2 = ChildNoteScript.correctNotes[0];
        slot1 = new Vector2(swap1.transform.position.x, swap1.transform.position.y);
        slot2 = new Vector2(swap2.transform.position.x, swap2.transform.position.y);
    }

    //see chaseSwap
    public void Execute() {
        chaseSwap();
        //if we lost our note or the swaps are complete, we're done swapping
        if (StealNotes.getStolenNote() == null) {
            finished = true;
        }

    }

    void chaseSwap() {
        //place note 1 in note 2's spot
        if (StealNotes.getStolenNote() == swap1) {
            transform.position = Vector2.MoveTowards(transform.position, slot2, Time.deltaTime * moveSpeed);
            if (Vector2.Distance(transform.position, slot2) <= 1f) {
                swapped1 = true;
                swap1.transform.position = new Vector3(slot2.x, slot2.y, 0);
                StealNotes.setStolenNote(swap2);
            }
        } else if (StealNotes.getStolenNote() == swap2) {
            //place note 2 in note 1's spot
            transform.position = Vector2.MoveTowards(transform.position, slot1, Time.deltaTime * moveSpeed);
            if (Vector2.Distance(transform.position, slot1) <= 1f) {
                swapped2 = true;
                swap2.transform.position = new Vector3(slot1.x, slot1.y, 0);
                StealNotes.setStolenNote(null);
            }
        }
    }

    //reset all variables
    public void Exit() {
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
