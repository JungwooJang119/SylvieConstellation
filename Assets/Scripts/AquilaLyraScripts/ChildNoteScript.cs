using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Attached to each note individually
* controls whether or not the notes are idle,
* follow Aquilla or follow the player
*/
public class ChildNoteScript : MonoBehaviour
{
    [SerializeField] private bool isCorrect = false; //is in its proper slot
    [SerializeField] private bool isSelected = false; //player has it
    [SerializeField] private bool isGot = false; //aquilla has it
    [SerializeField] private Transform player; //position where it follows the player
    [SerializeField] private Transform aquilla; //position where it follows aquilla
    [SerializeField] private Rigidbody2D rb; //its physics component

    [SerializeField] public static List<GameObject> correctNotes; //keeps track of all the notes that are in the right place
    //used for steal, swap, and determining when to end the puzzle

    public bool getCorrect() {
        return isCorrect;
    }

    public void setCorrect(bool b) {
        isCorrect = b;
    }

    public bool getSelected() {
        return isSelected;
    }

    public void setSelected(bool b) {
        isSelected = b;
    }

    public bool getGot() {
        return isGot;
    }

    public void setGot(bool b) {
        isGot = b;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        correctNotes = new List<GameObject>();
    }
    void FixedUpdate()
    {
        if (isCorrect) {
            //do nothing if its correct
        } else if (isSelected && !isGot) {
            //follow the player
            follow(player);
        } else if (isGot) {
            //follow aquilla
            follow(aquilla);
        } else {
            //do nothing otherwise
        }
    }
    public void follow(Transform target) {
        transform.position = target.position;
    }

    
}
