using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSwap : IState
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject swap1;
    [SerializeField] private GameObject swap2;
    [SerializeField] private Transform transform;
    [SerializeField] private bool finished;
    [SerializeField] private IState next;

    public StateSwap(Transform t) {
        transform = t;
        finished = false;
    }
    public void setNext(IState n) {
        next = n;
    }
    public void Enter() {
        Debug.Log("starting swap");
        finished = false;
        // swap1 = ChildNoteScript.correctNotes.Dequeue();
        // swap2 = ChildNoteScript.correctNotes.Dequeue();
    }

    public void Execute() {
        Debug.Log("swapping");
    }

    public void Exit() {
        Debug.Log("ending swap");
        swap1 = null;
        swap2 = null;
        finished = true;
    }
    public bool Finished() {
        return finished;
    }
    public IState getNext() {
        return next;
    }
}
