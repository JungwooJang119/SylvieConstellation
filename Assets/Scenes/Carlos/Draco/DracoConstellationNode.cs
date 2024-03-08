using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
#endif

public class DracoConstellationNode : MonoBehaviour {

    [SerializeField] private float speed;
    private DracoPathNode target;

    public void Init(DracoPathNode target) => this.target = target;

    void Update() {
        if (target == null) return;
        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        if (Mathf.Approximately(transform.position.x, target.position.x)
            && Mathf.Approximately(transform.position.y, target.position.y)) target = target.GetNext();
    }
}