using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSteal : IState
{
    [SerializeField] private GameObject current;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform transform;

    public StateSteal(Transform t) {
        transform = t;
    }
    public void Enter() {
        Debug.Log("entering steal");
        current = ChildNoteScript.correctNotes[0];
        ChildNoteScript.correctNotes.RemoveAt(0);
    }
    public void Execute() {
       transform.position = Vector2.MoveTowards(transform.position, current.transform.position, Time.deltaTime * moveSpeed);
    }
    public void Exit() {
        //Debug.Log("exiting steal");
        current = null;
    }
}
