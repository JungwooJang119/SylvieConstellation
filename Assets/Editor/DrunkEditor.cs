using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DrunkGoggles))]
public class DrunkEditor : Editor {
    public SerializedProperty intensity;
    
    void OnEnable() {
        intensity = serializedObject.FindProperty("intensity");
    }
    
    public override void OnInspectorGUI() {
        int prevIntensityValue = intensity.intValue;
        
        base.OnInspectorGUI();
        
        serializedObject.Update();
        
        DrunkGoggles handler = (DrunkGoggles) target;
        
        if (prevIntensityValue != intensity.intValue) {
            handler.transform.GetComponent<DrunkGoggles>().SetDrunkIntensity(intensity.intValue);
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
