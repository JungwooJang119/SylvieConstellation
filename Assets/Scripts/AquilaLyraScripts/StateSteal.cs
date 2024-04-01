using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSteal : IState
{
    [SerializeField] private GameObject current;
    [SerializeField] private float moveSpeed = 5f;

    public void Enter() {
        current = ChildNoteScript.correctNotes.Dequeue();
    }
    public void Execute() {
       transform.position = Vector2.MoveTowards(transform.position, current.transform.position, Time.deltaTime * moveSpeed);
    }
    public void Exit() {
        current = null;
    }
}
