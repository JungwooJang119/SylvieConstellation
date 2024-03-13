using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePlacement : MonoBehaviour
{
    [SerializeField] private GameObject childNote;
    [SerializeField] private GameObject slot;
    public static int notesCorrect;
    // Start is called before the first frame update
    void Start()
    {
        childNote = slot.transform.GetChild(0).gameObject;
        notesCorrect = 0;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == childNote) {
            childNote.transform.position = new Vector3(slot.transform.position.x, slot.transform.position.y, slot.transform.position.z);
            childNote.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            childNote.GetComponent<ChildNoteScript>().setCorrect(true);
            notesCorrect++;
        }
    }
}
