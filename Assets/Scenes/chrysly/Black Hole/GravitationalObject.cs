using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GravitationalObject : MonoBehaviour {
    [Header("Gravitation Parameters")] [Tooltip("")] 
    [SerializeField] private float eventHorizonRadius = 1f;

    [SerializeField] private float influenceRadius = 3f;

    [SerializeField] private float mass = 1f;

    private void Reset() {
        CreateEventHorizon();
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    /**
     * Creates an EventHorizon child under this transform. Yeah, I could've made you do it yourself in the editor but
     * I did it for you, you're welcome...
     */
    private void CreateEventHorizon() {
        if (transform.GetComponentInChildren<EventHorizon>() == null) {
            GameObject eventHorizon = Instantiate(new GameObject("Event Horizon"), transform.position,
                Quaternion.identity, transform);
            eventHorizon.AddComponent<EventHorizon>();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        other.transform.GetComponent<GravitationResponder>()?.Gravitate(transform.position, mass);
    }

    #region GIZMOS
    void OnDrawGizmos()
    {
        Handles.color = Color.magenta;
        Handles.DrawWireDisc(transform.position, Vector3.forward, eventHorizonRadius);
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, Vector3.forward, influenceRadius);
    }
    #endregion GISMOS
}
