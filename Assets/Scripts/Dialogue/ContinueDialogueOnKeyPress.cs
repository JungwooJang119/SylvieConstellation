using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ContinueDialogueOnKeyPress : MonoBehaviour
{
    [SerializeField] UnityEvent continueDialogueEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

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
