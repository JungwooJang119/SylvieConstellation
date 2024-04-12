using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DracoStarAnimator : MonoBehaviour {

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxDislocation;
    [SerializeField] private Vector2 dislocationInterval;

    private Vector2 smoothSpeed;
    private Vector2 dislocationTarget;

    void Start() {
        transform.localScale = Vector2.zero;
        transform.DOScale(Vector2.one, 1.2f).SetEase(Ease.OutElastic);
        StartCoroutine(_DislocationRandomizer());
    }

    void Update() {
        if (rotationSpeed > 0.1f) transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationSpeed));
        if (!Mathf.Approximately(maxDislocation, 0)) {
            transform.localPosition = Vector2.SmoothDamp(transform.localPosition, dislocationTarget, ref smoothSpeed, 1);
        }
    }

    private IEnumerator _DislocationRandomizer() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(dislocationInterval.x, dislocationInterval.y));
            dislocationTarget = new Vector2(Random.Range(0, maxDislocation), Random.Range(0, maxDislocation));
        }
    }
}