using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cm1;
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D other) {
        cm1.Follow = player.transform;
    }
}
