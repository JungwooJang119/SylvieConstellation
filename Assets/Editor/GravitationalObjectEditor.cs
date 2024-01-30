using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GravitationalObject))]
public class GravitationalObjectEditor : Editor
{
    public SerializedProperty eventHorizonRadius;
    public SerializedProperty influenceRadius;
    
    void OnEnable() {
        eventHorizonRadius = serializedObject.FindProperty("eventHorizonRadius");
        influenceRadius = serializedObject.FindProperty("influenceRadius");
    }
    
    public override void OnInspectorGUI() {
        float prevHorizonValue = eventHorizonRadius.floatValue;
        float prevInfluenceValue = influenceRadius.floatValue;
        
        base.OnInspectorGUI();
        
        serializedObject.Update();
        
        GravitationalObject handler = (GravitationalObject) target;
        
        if (!Mathf.Approximately(prevHorizonValue, eventHorizonRadius.floatValue)) {
            if (eventHorizonRadius.floatValue > influenceRadius.floatValue) {
                eventHorizonRadius.floatValue = influenceRadius.floatValue;
            }
            AdjustCollider(eventHorizonRadius.floatValue,
                handler.transform.GetComponentInChildren<EventHorizon>().GetComponent<CircleCollider2D>());
        }

        if (!Mathf.Approximately(prevInfluenceValue, influenceRadius.floatValue)) {
            if (eventHorizonRadius.floatValue > influenceRadius.floatValue) {
                eventHorizonRadius.floatValue = influenceRadius.floatValue;
                AdjustCollider(eventHorizonRadius.floatValue,
                    handler.transform.GetComponentInChildren<EventHorizon>().GetComponent<CircleCollider2D>());
            }
            AdjustCollider(influenceRadius.floatValue, handler.transform.GetComponent<CircleCollider2D>());
        }

        if (eventHorizonRadius.floatValue < 0) eventHorizonRadius.floatValue = 0;
        if (influenceRadius.floatValue < 0) influenceRadius.floatValue = 0;
        
        serializedObject.ApplyModifiedProperties();
    }
    
    private void AdjustCollider(float radius, CircleCollider2D collider) {
        collider.radius = radius;
    }
}
