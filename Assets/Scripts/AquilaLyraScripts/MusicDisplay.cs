using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Attached to Lyra
* Displays where the notes are supposed to
*/
public class MusicDisplay : MonoBehaviour
{
    [SerializeField] GameObject display; //the notes in their proper places
    //if the player is close to Lyra, display the notes
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            display.SetActive(true);
        }
    }

    //stop displaying them when the player leaves
    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag =="Player") {
            display.SetActive(false);
        }
    }

}
