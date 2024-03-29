using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Vector2 target;
    public GameObject pointer;
    public float HideDistance;
    void Start() {
        target = TransitionManager.Instance.targetPosition;
    }

    void Update()
    {
        target = TransitionManager.Instance.targetPosition;
        var dir = target - (Vector2)transform.position;

        if (dir.magnitude <= HideDistance)
        {
            pointer.SetActive(false);
        }
        else
        {
            pointer.SetActive(true);
        }

        var angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        pointer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
