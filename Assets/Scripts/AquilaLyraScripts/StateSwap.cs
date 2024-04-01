using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSwap : IState
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject swap1;
    [SerializeField] private GameObject swap2;

    public void Enter() {
        swap1 = ChildNoteScript.correctNotes.Dequeue();
        swap2 = ChildNoteScript.correctNotes.Dequeue();
    }

    public void Execute() {
        Debug.Log("swapping");
    }

    public void Exit() {
        swap1 = null;
        swap2 = null;
    }
}
