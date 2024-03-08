using UnityEngine;
using DG.Tweening;

public class DracoStarAnimator : MonoBehaviour {

    [SerializeField] private float rotationSpeed;

    void Start() {
        transform.localScale = Vector2.zero;
        transform.DOScale(Vector2.one, 1.2f).SetEase(Ease.OutElastic);
    }

    void Update() {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationSpeed));
    }
}
