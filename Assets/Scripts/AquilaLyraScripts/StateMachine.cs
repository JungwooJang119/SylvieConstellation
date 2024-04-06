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
public class StateMachine : MonoBehaviour
{
    IState curState;

    public void ChangeState(IState next) {
        if (curState != null) {
            curState.Exit();
        }
        curState = next;
        curState.Enter();
    }    
    
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
}
