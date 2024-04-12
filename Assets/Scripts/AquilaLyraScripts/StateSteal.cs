using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Comes after idle
* removed a note from its correct spot
*/
public class StateSteal : IState
{
    [SerializeField] private GameObject current; //the note we're stealing
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform transform; //aquilla's position
    [SerializeField] private bool finished; //did we claim the note
    [SerializeField] private IState idle; //no other notes to steal, go to idle
    [SerializeField] private IState swap; //more notes to steal, swap

    public StateSteal(Transform t) {
        transform = t;
        finished = false;
    }
    public void setNext(IState i, IState s) {
        idle = i;
        swap = s;
    }
    //initialize what note we're stealing
    public void Enter() {
        finished = false;
        //drop our current note if we have one
        StealNotes.setStolenNote(null);
        current = ChildNoteScript.correctNotes[0];
    }
    //move towards our desired note
    public void Execute() {
        transform.position = Vector2.MoveTowards(transform.position, current.transform.position, Time.deltaTime * moveSpeed);
        //stop when we have it
        if (StealNotes.hasStolenNote()) {
            finished = true;
        }
    }

    public GameObject getCurrentNote() {
        return current;
    }
    public void Exit() {
        current = null;
        finished = true;
    }

    public bool Finished() {
        return finished;
    }
    public IState getNext() {
        if (ChildNoteScript.correctNotes.Count != 0) {
            return swap;
        } else {
            return idle;
        }
    }
}
