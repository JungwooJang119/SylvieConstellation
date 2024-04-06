using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR;

public class CreditsObject : MonoBehaviour
{
    private Vector3 update;
    [SerializeField] private Transform child;
    [SerializeField] private float y_moveSpeed = 2f;
    [SerializeField] private float x_moveSpeed = 0f;
    
    [SerializeField] private int numRotations = 5;
    [SerializeField] private float rotationTime = 5f;
    
    private bool interacted;
    private bool rotated;
    
    void Update()
    {
        update = new Vector3(Time.deltaTime * x_moveSpeed, Time.deltaTime * y_moveSpeed, 0.0f);
        transform.position += update;
        if (interacted && !rotated)
        {
            StartCoroutine(Rotate());
            rotated = true;
        }
    }

    // Coroutine to rotate the object
    IEnumerator Rotate()
    {
        for (int i = 0; i < numRotations; i++)
        {
            float timeElapsed = 0;
            // rotation implementation
            while (timeElapsed < rotationTime)
            {
                float angle = Mathf.Lerp(0, 360, timeElapsed / rotationTime);
                child.transform.rotation = Quaternion.Euler(0, 0, angle);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        // resets to normal afterward
        child.transform.rotation = Quaternion.Euler(0, 0, 360);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
            if (!interacted)
            {
                Debug.Log("i am glowing (me when i lie)"); 
                interacted = true;
            }
    }
}