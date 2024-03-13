using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectNote : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private int axisScaler = 10;
    [SerializeField] private Transform rotateAround;

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.name.Contains("note") && !col.gameObject.GetComponent<ChildNoteScript>().getCorrect()) {
            // inputValue = input.Player.OrbitControl.triggered;
            if (Input.GetKey(KeyCode.Q))
            {
                col.transform.RotateAround(rotateAround.position, Vector3.forward * axisScaler, rotationSpeed);
            }

            // When button releases, push object. TODO
            if (Input.GetKeyUp(KeyCode.Q))
            {
                col.attachedRigidbody.AddForce(Vector3.forward * 1000000);
            }
            
        }
    }
}
