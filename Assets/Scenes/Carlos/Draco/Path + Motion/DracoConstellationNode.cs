using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
#endif

[RequireComponent(typeof(Rigidbody2D))]
public class DracoConstellationNode : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float weight;
    [SerializeField] private float targetTolerance;
    private DracoPathNode target;
    private Rigidbody2D rb;

    private Vector2 direction;
    public Vector2 Direction => direction;

    public void Init(DracoPathNode target) {
        this.target = target;
        rb = GetComponent<Rigidbody2D>();
        direction = (target.position - (Vector2) transform.position).normalized;
    }

    void Update() {
        if (target == null) return;
        if (Vector2.Distance(transform.position, target.position) < targetTolerance) target = target.GetNext();
    }

    void FixedUpdate() {
        if (target == null) return;
        rb.velocity = Vector2.one * speed * direction;
        direction = Vector2.MoveTowards(direction, (target.position - (Vector2) transform.position).normalized, Time.deltaTime * weight);
    }
}