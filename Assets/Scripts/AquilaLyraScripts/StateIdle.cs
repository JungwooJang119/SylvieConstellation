using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IState
{
    [SerializeField] private Transform randomTarget;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float timeLeft;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    public void Enter() {
        randomTarget.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        timeLeft = 3f;
    }
    public void Execute() {
        timeLeft -= Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, randomTarget.position, Time.deltaTime * moveSpeed);
        if(timeLeft < 0) {
            Enter();
        }
    }
    public void Exit() {
        Debug.Log("switching out of Idle");
    }
}
