using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Attached to each music slot
*/
public class NotePlacement : MonoBehaviour
{
    [SerializeField] private GameObject childNote; //the note that's supposed to be here
    [SerializeField] private GameObject slot; //the slot itself
    // Start is called before the first frame update
    void Start()
    {
        childNote = slot.transform.GetChild(0).gameObject;
    }

    void FixedUpdate() {
        //for now, the level freezes when they win
        //CHANGE FOR ACTUAL GAME
        if (ChildNoteScript.correctNotes.Count == 10) {
            Debug.Log("You win!");
            Time.timeScale = 0f;
        }
    }
    //when something collides with the slot
    void OnTriggerEnter2D(Collider2D other) {
        //if the player placed the note in the correct slot
        if (other.gameObject == childNote && !other.gameObject.GetComponent<ChildNoteScript>().getGot()) {
            //lock it in place
            childNote.transform.position = new Vector3(slot.transform.position.x, slot.transform.position.y, slot.transform.position.z);
            childNote.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            //make sure the note knows its right
            childNote.GetComponent<ChildNoteScript>().setCorrect(true);
            childNote.GetComponent<ChildNoteScript>().setSelected(false);
            childNote.GetComponent<ChildNoteScript>().setGot(false);
            //add it to the list of correct notes for aquilla mechanics and tracking
            ChildNoteScript.correctNotes.Add(childNote);
        }
    }
}
