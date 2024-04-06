using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSteal : IState
{
    [SerializeField] private GameObject current;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform transform;
    [SerializeField] private bool finished;
    [SerializeField] private IState next;

    public StateSteal(Transform t) {
        transform = t;
        finished = false;
    }
    public void setNext(IState n) {
        next = n;
    }
    public void Enter() {
        Debug.Log("entering steal");
        finished = false;
        StealNotes.setStolenNote(null);
        current = ChildNoteScript.correctNotes[0];
        ChildNoteScript.correctNotes.RemoveAt(0);
    }
    public void Execute() {
        transform.position = Vector2.MoveTowards(transform.position, current.transform.position, Time.deltaTime * moveSpeed);
        if (StealNotes.hasStolenNote()) {
            finished = true;
        }
    }
    public void Exit() {
        current = null;
        finished = true;
    }

    public bool Finished() {
        return finished;
    }
    public IState getNext() {
        return next;
    }
}
