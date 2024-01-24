using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody2D))]
public class GravitationResponder : MonoBehaviour
{
    private const float GConstant = 6.672e-11f;
    private Rigidbody2D _rigidbody;
    [SerializeField] private bool displayInput = false;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Gravitate(Vector3 position, float mass) {
        _rigidbody.AddForce(GAcceleration(position, mass));
    }

    private Vector3 GAcceleration(Vector3 position, float mass) {
        Vector3 direction = position - transform.position;
        
        float gravityForce = GConstant * ((mass * _rigidbody.mass) / direction.sqrMagnitude);
        gravityForce /= _rigidbody.mass;
 
        return direction.normalized * gravityForce * Time.fixedDeltaTime;
    }

    private void OnDrawGizmos() {
        if (displayInput) {
            Gizmos.color = Color.magenta;
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized;
            Gizmos.DrawLine(transform.position, transform.position + inputDirection * 3);
            Gizmos.color = Color.yellow;
            if (_rigidbody != null) {
                Gizmos.DrawLine(transform.position,
                    transform.position + new Vector3(_rigidbody.totalForce.x, _rigidbody.totalForce.y, 0f));
            }
        }
    }
}
