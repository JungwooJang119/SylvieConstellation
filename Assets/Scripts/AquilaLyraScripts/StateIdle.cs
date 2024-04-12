using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Default Aquilla's movement
* just patrols the map and collects notes
* if we run into them
*/
public class StateIdle : IState
{
    [SerializeField] private Transform randomTarget; //the random position for patrolling
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float startTime; //keeps track of if we need to move elsehwere or not
    [SerializeField] private float duration = 3f;
    [SerializeField] private float minX = 114f; //bounds
    [SerializeField] private float maxX = 140f;
    [SerializeField] private float minY = -24f;
    [SerializeField] private float maxY = -10f;

    [SerializeField] private Transform transform; //aquilla's position
    [SerializeField] IState stealState; //if theres a note to steal, stop patrolling
    [SerializeField] private bool finished;

    public StateIdle(Transform t, Transform rt) {
        transform = t;
        randomTarget = rt;
        finished = false;
    }
    public void setNext(IState steal) {
        stealState = steal;
    }
    //initiliaze the first random roaming spot
    public void Enter() {
        randomTarget.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        startTime = Time.time;
        finished = false;
    }

    //move towards the random roaming spot, and move the spot if we need to
    public void Execute() {
        if (Time.time >= startTime + duration || transform.position == randomTarget.position) {
            if (ChildNoteScript.correctNotes.Count > 0) {
                finished = true;
            } else {
                Enter();
            }
            
        }
        transform.position = Vector2.MoveTowards(transform.position, randomTarget.position, Time.deltaTime * moveSpeed);
        
    }
    public IState getNext() {
        return stealState;
    }
    public void Exit() {
        finished = true;
        StealNotes.setStolenNote(null);
    }
    public bool Finished() {
        return finished;
    }
}
