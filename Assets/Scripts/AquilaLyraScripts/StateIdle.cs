using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IState
{
    [SerializeField] private Transform randomTarget;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float startTime;
    [SerializeField] private float duration = 3f;
    [SerializeField] private float minX = 114f;
    [SerializeField] private float maxX = 140f;
    [SerializeField] private float minY = -24f;
    [SerializeField] private float maxY = -10f;

    [SerializeField] private Transform transform;

    
    public StateIdle(Transform t, Transform rt) {
        transform = t;
        randomTarget = rt;
    }
    public void Enter() {
        randomTarget.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        startTime = Time.time;
    }
    public void Execute() {
        if (changeTarget() || transform.position == randomTarget.position) {
            Enter();
        }
        transform.position = Vector2.MoveTowards(transform.position, randomTarget.position, Time.deltaTime * moveSpeed);
    }
    public void Exit() {
        Debug.Log("switching out of Idle");
    }
    private bool changeTarget() {
        return Time.time >= startTime + duration;
    }
}
