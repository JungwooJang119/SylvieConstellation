using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attached to aquilla to control her movement
public class StealNotes : MonoBehaviour
{
    [SerializeField] private static GameObject stolenNote; //the note aquilla has
    [SerializeField] private Transform randomTarget; //the random spot for patrolling
    [SerializeField] private StateMachine sm; //state machine to control behavior
    [SerializeField] private StateIdle idle; //idle behavior
    [SerializeField] private StateSteal steal; //stealing behavior
    [SerializeField] private StateSwap swap; //swapping behavior
    [SerializeField] private float coolDown; //buffer time between states to prevent transitions that are too fast to process

    void Start() {
        sm = new StateMachine();
        idle = new StateIdle(GetComponent<Transform>(), randomTarget);
        steal = new StateSteal(GetComponent<Transform>());
        swap = new StateSwap(GetComponent<Transform>());
        idle.setNext(steal);
        steal.setNext(idle, swap);
        swap.setNext(idle);
        sm.ChangeState(idle);
        coolDown = Time.deltaTime;
    }

    void FixedUpdate() {
        //let the SM do its thing
        sm.Update();
        //if our note was taken, set our current note to null
        if (stolenNote.GetComponent<ChildNoteScript>().getSelected()) {
            setStolenNote(null);
        }
    }

    //return if we have a note at all
    public static bool hasStolenNote() {
        return stolenNote != null;
    }
    //return the exact note we have
    public static GameObject getStolenNote() {
        return stolenNote;
    }
    //reset our notes if we need to
    public static void setStolenNote(GameObject note) {
        updateCurrent(false);
        stolenNote = note;
        updateCurrent(true);
    }
    static void updateCurrent(bool b) {
        if (stolenNote != null) {
            if (stolenNote.GetComponent<ChildNoteScript>().getCorrect()) {
                ChildNoteScript.correctNotes.Remove(stolenNote);
                stolenNote.GetComponent<ChildNoteScript>().setCorrect(false);
            }
            stolenNote.GetComponent<ChildNoteScript>().setGot(b);
        }
    }
    
    //Aquilla's collision behavior
    void OnTriggerStay2D(Collider2D col) {
        //we collide with a note and we don't currently have one
        if (col.gameObject.name.Contains("note") && stolenNote == null) {
            //if its the note that we want to steal, steal it
            if (sm.currentState() == steal && sm.isRunning() && steal.getCurrentNote() == col.gameObject) {
                setStolenNote(col.gameObject);
                coolDown = Time.deltaTime;
            } else if (sm.currentState() == idle && Time.deltaTime - coolDown > 3f) {
                //if we're not stealing, take this one
                setStolenNote(col.gameObject);
                coolDown = Time.deltaTime;
            }
        } else if(sm.currentState() == swap && (swap.getSwap1() == col.gameObject || swap.getSwap2() == col.gameObject)) {
            //if we're swapping and we hit the note we want to swap with
            if (stolenNote != null) {
                //place our current note in its desire spot
                stolenNote.transform.position = new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y, col.gameObject.transform.position.z);
                if (stolenNote == swap.getSwap2()) {
                    coolDown = Time.deltaTime;
                }
            }
            setStolenNote(col.gameObject);
        }
    }

}
