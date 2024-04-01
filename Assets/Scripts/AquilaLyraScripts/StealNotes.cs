using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StealNotes : MonoBehaviour
{
    [SerializeField] private GameObject stolenNote;
    [SerializeField] private StateMachine sm;
    [SerializeField] private StateIdle idle;
    [SerializeField] private StateSteal steal;
    [SerializeField] private StateSwap swap;

    void Start() {
        sm = new StateMachine();
        idle = new StateIdle();
        steal = new StateSteal();
        swap = new StateSwap();

        sm.ChangeState(idle);
    }

    void FixedUpdate() {
        if (ChildNoteScript.correctNotes.Count == 0 && sm.currentState() != idle) {
            sm.ChangeState(idle);
        } e
    }
    void oldControl() {
        if(ChildNoteScript.correctNotes.Count != 0 && stolenNote == null && current == null) {
            current = ChildNoteScript.correctNotes.Dequeue();
        } 
        if (current != null && stolenNote == null) {
            chaseNote();
        } else {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                randomTarget.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                timeLeft = 3f;
            }
           randomMovement();
        }
    }

    void randomMovement() {
        transform.position = Vector2.MoveTowards(transform.position, randomTarget.position, Time.deltaTime * moveSpeed);
        if(Vector2.Distance(transform.position, randomTarget.position) < 0.2f) {
            timeLeft = 0;
        }
    }
    void chaseNote() {
        transform.position = Vector2.MoveTowards(transform.position, current.transform.position, Time.deltaTime * moveSpeed);
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
            current = null;
            stolenNote.GetComponent<ChildNoteScript>().setSelected(false);
            stolenNote.GetComponent<ChildNoteScript>().setGot(true);
            stolenNote.GetComponent<ChildNoteScript>().setCorrect(false);
            sm.ChangeState(idle);
        }
        updateCurrent();
    }

    

}
