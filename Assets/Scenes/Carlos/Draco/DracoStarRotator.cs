using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DracoStarRotator : MonoBehaviour {

    private DracoConstellationNode parentNode;

    private Vector2 angularDirection;
    private Vector2 angularSpeed;

    void Start() {
        parentNode = GetComponentInParent<DracoConstellationNode>();
    }

    void Update() {
        angularDirection = Vector2.SmoothDamp(angularDirection, parentNode.Direction, ref angularSpeed, 1);
        Quaternion rotation = Quaternion.LookRotation(angularDirection, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
}
