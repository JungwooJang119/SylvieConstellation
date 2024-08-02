using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ContinueDialogueOnKeyPress : MonoBehaviour
{
    [SerializeField] private UnityEvent continueDialogueEvent;

    // Update is called once per frame
    void Update()
    {
        bool continuing = Keyboard.current.spaceKey.wasPressedThisFrame;
        if (continuing)
        {
            continueDialogueEvent.Invoke();
        }
    }
}
