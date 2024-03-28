using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePlacement : MonoBehaviour
{
    [SerializeField] private GameObject childNote;
    [SerializeField] private GameObject slot;
    // Start is called before the first frame update
    void Start()
    {
        childNote = slot.transform.GetChild(0).gameObject;
    }

    void FixedUpdate() {
        //ISSUE: sometimes the game ends prematurely -> fix!!!
        if (ChildNoteScript.correctNotes.Count == 10) {
            Debug.Log("You win!");
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == childNote && !other.gameObject.GetComponent<ChildNoteScript>().getGot()) {
            childNote.transform.position = new Vector3(slot.transform.position.x, slot.transform.position.y, slot.transform.position.z);
            childNote.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            childNote.GetComponent<ChildNoteScript>().setCorrect(true);
            ChildNoteScript.correctNotes.Enqueue(childNote);
            Debug.Log(ChildNoteScript.correctNotes.Count);
        }
    }
}
