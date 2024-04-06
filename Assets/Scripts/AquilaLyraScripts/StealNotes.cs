using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StealNotes : MonoBehaviour
{
    [SerializeField] private static GameObject stolenNote;
    [SerializeField] private Transform randomTarget;
    [SerializeField] private StateMachine sm;
    [SerializeField] private StateIdle idle;
    [SerializeField] private StateSteal steal;
    [SerializeField] private StateSwap swap;

    void Start() {
        sm = new StateMachine();
        idle = new StateIdle(GetComponent<Transform>(), randomTarget);
        steal = new StateSteal(GetComponent<Transform>());
        swap = new StateSwap(GetComponent<Transform>());
        idle.setNext(steal);
        steal.setNext(idle);
        swap.setNext(idle);
        sm.ChangeState(idle);
    }

    void FixedUpdate() {
        sm.Update();
    }

    public static bool hasStolenNote() {
        return stolenNote != null;
    }
    public static void setStolenNote(GameObject note) {
        if (stolenNote != null) {
            stolenNote.GetComponent<ChildNoteScript>().setCorrect(false);
            stolenNote.GetComponent<ChildNoteScript>().setGot(false);
            stolenNote.GetComponent<ChildNoteScript>().setSelected(false);
        }
        stolenNote = note;
        if (stolenNote != null) {
            stolenNote.GetComponent<ChildNoteScript>().setGot(true);
            stolenNote.GetComponent<ChildNoteScript>().setCorrect(false);
        }
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
            if (stolenNote.GetComponent<ChildNoteScript>().getCorrect()) {
                stolenNote.GetComponent<ChildNoteScript>().setCorrect(false);
                ChildNoteScript.correctNotes.Remove(stolenNote);
            }
            stolenNote.GetComponent<ChildNoteScript>().setSelected(false);
            stolenNote.GetComponent<ChildNoteScript>().setGot(true);
        }
        updateCurrent();
    }

    

}
