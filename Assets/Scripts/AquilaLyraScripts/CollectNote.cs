using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectNote : MonoBehaviour
{

    
    [SerializeField] public GameObject currentNote = null;

    void OnTriggerStay2D(Collider2D col) {
        
        if (col.gameObject.name.Contains("note") && !col.gameObject.GetComponent<ChildNoteScript>().getCorrect() && currentNote == null) {
            currentNote = col.gameObject;
            currentNote.GetComponent<ChildNoteScript>().setSelected(true);
            currentNote.GetComponent<ChildNoteScript>().setGot(false);
        }
    }
    void FixedUpdate() {
        updateCurrent();
    }

    void updateCurrent() {
        if(currentNote != null) {
            if (currentNote.GetComponent<ChildNoteScript>().getGot() || currentNote.GetComponent<ChildNoteScript>().getCorrect()) {
                currentNote.GetComponent<ChildNoteScript>().setSelected(false);
                currentNote = null;
            }
        }
    }
}
