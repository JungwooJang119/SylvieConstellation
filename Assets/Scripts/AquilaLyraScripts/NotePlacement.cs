using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePlacement : MonoBehaviour
{
    [SerializeField] private GameObject childNote;
    [SerializeField] private GameObject slot;
    [SerializeField] public static int notesCorrect = 0;
    // Start is called before the first frame update
    void Start()
    {
        childNote = slot.transform.GetChild(0).gameObject;
        notesCorrect = 0;
    }

    void Update() {
        if (notesCorrect == 10) {
            Debug.Log("You win!");
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == childNote && !other.gameObject.GetComponent<ChildNoteScript>().getGot()) {
            childNote.transform.position = new Vector3(slot.transform.position.x, slot.transform.position.y, slot.transform.position.z);
            childNote.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            childNote.GetComponent<ChildNoteScript>().setCorrect(true);
            notesCorrect++;
            ChildNoteScript.correctNotes.Enqueue(childNote);
            Debug.Log("notes correct: " + ChildNoteScript.correctNotes.Count);
            foreach(GameObject note in ChildNoteScript.correctNotes) {
                Debug.Log(note.name);
            }
        }
    }
}
