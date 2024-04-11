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
        idle.setNext(steal, swap);
        steal.setNext(idle, swap);
        swap.setNext(idle);
        sm.ChangeState(idle);
    }

    void FixedUpdate() {
        sm.Update();
    }

    public static bool hasStolenNote() {
        return stolenNote != null;
    }
    public static GameObject getStolenNote() {
        return stolenNote;
    }
    public static void setStolenNote(GameObject note) {
        updateCurrent(false);
        stolenNote = note;
        updateCurrent(true);
    }
    static void updateCurrent(bool b) {
        if (stolenNote != null) {
            if (stolenNote.GetComponent<ChildNoteScript>().getCorrect()) {
                ChildNoteScript.correctNotes.RemoveAt(0);
                stolenNote.GetComponent<ChildNoteScript>().setCorrect(false);
            }
            stolenNote.GetComponent<ChildNoteScript>().setGot(b);
            stolenNote.GetComponent<ChildNoteScript>().setSelected(false);
        }
    }
    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.name.Contains("note") && stolenNote == null) {
            if (sm.currentState() == steal && sm.isRunning() && steal.getCurrentNote() == col.gameObject) {
                setStolenNote(col.gameObject);
            } else if (sm.currentState() == idle) {
                setStolenNote(col.gameObject);
            }
        } else if(sm.currentState() == swap && (swap.getSwap1() == col.gameObject || swap.getSwap2() == col.gameObject)) {
                if (stolenNote != null) {
                    stolenNote.transform.position = new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y, col.gameObject.transform.position.z);
                    if (stolenNote == swap.getSwap1()) {
                        swap.setSwapped1(true);
                    } else if (stolenNote == swap.getSwap2()) {
                        swap.setSwapped2(true);
                    }
                }
                setStolenNote(col.gameObject);
        }
    }

}
