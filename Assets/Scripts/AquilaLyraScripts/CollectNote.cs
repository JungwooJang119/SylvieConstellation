using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Attached to the player
* keeps track of whether or not a "note"
* is following the player or not
*/
public class CollectNote : MonoBehaviour
{
    //oject variable for the note the player has
    [SerializeField] public GameObject currentNote = null;

    //initialize the player position to avoid visual glitches (might need more fixing)
    void Start() {
        transform.position = new Vector3(115.72f, -23.18f, 0f);
    }

    //When the player collides with something
    void OnTriggerStay2D(Collider2D col) {
        //if we collided with a note that is not yet fixed and we don't have a note yet
        if (col.gameObject.name.Contains("note") && !col.gameObject.GetComponent<ChildNoteScript>().getCorrect() && currentNote == null) {
            //the note is now ours mwahaha
            currentNote = col.gameObject;
            //make sure the note knows it is now ours
            currentNote.GetComponent<ChildNoteScript>().setSelected(true);
            //make sure the note knows that it is not aquilla's
            currentNote.GetComponent<ChildNoteScript>().setGot(false);
        }
    }

    void FixedUpdate() {
        updateCurrent();
    }

    //if our not gets taken, make sure that its not in our inventory anymore
    void updateCurrent() {
        if(currentNote != null) {
            if (currentNote.GetComponent<ChildNoteScript>().getGot() || currentNote.GetComponent<ChildNoteScript>().getCorrect()) {
                currentNote.GetComponent<ChildNoteScript>().setSelected(false);
                currentNote = null;
            }
        }
    }
}
