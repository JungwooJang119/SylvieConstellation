using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EventHorizon : MonoBehaviour
{
    private void Reset() {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Sylvie got wrecked by black hole");
        // Destroy(other.gameObject);  //TODO: Replace this with whatever u guys want to happen when Sylvie ded
        PlayerController.Instance.DeathSequence();
    }
}
