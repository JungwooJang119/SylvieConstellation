using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSwap : IState
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject swap1;
    [SerializeField] private GameObject swap2;

    public void Enter() {
        Debug.Log("starting swap");
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
    }
}
