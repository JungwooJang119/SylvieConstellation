using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSteal : IState
{
    [SerializeField] private GameObject current;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform transform;
    [SerializeField] private bool finished;
    [SerializeField] private IState idle;
    [SerializeField] private IState swap;

    public StateSteal(Transform t) {
        transform = t;
        finished = false;
    }
    public void setNext(IState i, IState s) {
        idle = i;
        swap = s;
    }
    public void Enter() {
        Debug.Log("entering steal");
        finished = false;
        StealNotes.setStolenNote(null);
        current = ChildNoteScript.correctNotes[0];
    }
    public void Execute() {
        transform.position = Vector2.MoveTowards(transform.position, current.transform.position, Time.deltaTime * moveSpeed);
        if (StealNotes.hasStolenNote()) {
            finished = true;
            //ChildNoteScript.correctNotes.RemoveAt(0);
        }
    }

    public GameObject getCurrentNote() {
        return current;
    }
    public void Exit() {
        Debug.Log("exiting steal");
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
