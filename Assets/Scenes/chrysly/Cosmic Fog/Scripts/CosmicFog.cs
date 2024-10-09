using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CosmicFog : MonoBehaviour {
    private CircleCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private bool _isActive = false;

    private Metaball2D _metaball;
    // Start is called before the first frame update
    private void Awake() {
        _collider = GetComponent<CircleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider.radius = 0.3f;
    }

    public virtual void Activate(float radius, float lifetime, Vector2 velocity, Vector3 position) {
        if (_isActive) return;
        
        _metaball = gameObject.AddComponent<Metaball2D>();
        _isActive = true;
        _collider.radius = 0.3f;
        DOTween.To(() => _collider.radius, x => _collider.radius = x, radius, lifetime / 2).SetEase(Ease.OutBack);
        transform.position = position;
        _rigidbody.velocity = velocity;
        StartCoroutine(DeactivateAction(lifetime));
    }

    protected virtual IEnumerator DeactivateAction(float lifetime) {
        yield return new WaitForSeconds(lifetime);
        DOTween.To(() => _collider.radius, x => _collider.radius = x, 0.3f, 1f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(1f);
        _isActive = false;
        Destroy(_metaball);
        _metaball = null;
    }

    public bool Active() {
        return _isActive;
    }
}
