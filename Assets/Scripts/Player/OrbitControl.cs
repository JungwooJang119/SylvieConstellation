using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbitControl : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private int axisScaler = 10;
    [SerializeField] private Transform rotateAround;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        // inputValue = input.Player.OrbitControl.triggered;
        if (Input.GetKey(KeyCode.Q))
        {
            other.transform.RotateAround(rotateAround.position, Vector3.forward * axisScaler, rotationSpeed);
        }

        // When button releases, push object. TODO
        if (Input.GetKeyUp(KeyCode.Q))
        {
            other.attachedRigidbody.AddForce(Vector3.forward * 1000000);
        }
    }
    
}
