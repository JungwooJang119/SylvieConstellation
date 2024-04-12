using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState{
    void Enter();
    void Execute();
    void Exit();
    bool Finished();
    IState getNext();
}
/*
* Flow of states: idle <-> steal -> swap -> idle
*/
public class StateMachine : MonoBehaviour
{
    //the current state
    IState curState;

    //transition
    public void ChangeState(IState next) {
        if (curState != null) {
            curState.Exit();
        }
        curState = next;
        curState.Enter();
    }    
    
    //execute the current state or transition if we finished
    public void Update() {
        if (curState != null && !curState.Finished()) {
            curState.Execute();
        } else if (curState != null && curState.Finished()) {
            ChangeState(curState.getNext());
        }
    }

    public IState currentState() {
        return curState;
    }

    public bool isRunning() {
        return !curState.Finished();
    }
}
