using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildNoteScript : MonoBehaviour
{
    [SerializeField] private bool isCorrect = false;

    public bool getCorrect() {
        return isCorrect;
    }

    public void setCorrect(bool b) {
        isCorrect = b;
    }
}
