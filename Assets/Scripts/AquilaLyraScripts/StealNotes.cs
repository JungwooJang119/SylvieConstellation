using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StealNotes : MonoBehaviour
{
    [SerializeField] private GameObject stolenNote;
    [SerializeField] private Transform randomTarget;
    [SerializeField] private StateMachine sm;
    [SerializeField] private StateIdle idle;
    [SerializeField] private StateSteal steal;
    [SerializeField] private StateSwap swap;

    void Start() {
        sm = new StateMachine();
        idle = new StateIdle(GetComponent<Transform>(), randomTarget);
        steal = new StateSteal(GetComponent<Transform>());
        swap = new StateSwap();

        sm.ChangeState(idle);
    }

    void FixedUpdate() {
        if (ChildNoteScript.correctNotes.Count != 0 && sm.currentState() == idle) {
            Debug.Log("break 1");
            if (stolenNote != null) {
                Debug.Log("setting to null");
                stolenNote.GetComponent<ChildNoteScript>().setSelected(false);
                stolenNote.GetComponent<ChildNoteScript>().setGot(false);
                stolenNote.GetComponent<ChildNoteScript>().setCorrect(false);
                stolenNote = null;
            }
            Debug.Log(stolenNote == null);
            sm.ChangeState(steal);
        } else if (stolenNote != null && sm.currentState() != idle) {
            sm.ChangeState(idle);
        }
        sm.Update();
    }

    void updateCurrent() {
        if(stolenNote != null) {
            if (stolenNote.GetComponent<ChildNoteScript>().getSelected()) {
                stolenNote = null;
            }
        }
    }
    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.name.Contains("note") && stolenNote == null) {
            stolenNote = col.gameObject;
            stolenNote.GetComponent<ChildNoteScript>().setSelected(false);
            stolenNote.GetComponent<ChildNoteScript>().setGot(true);
            stolenNote.GetComponent<ChildNoteScript>().setCorrect(false);
        }
        updateCurrent();
    }

    

}
