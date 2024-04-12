using System.Collections;
using UnityEngine;

public class TwinkleAnimator : MonoBehaviour {

    [Tooltip("Expected values between 0 and 1, or close to 1;")]
    [SerializeField] private Vector2 resizeBounds;
    [Tooltip("Expected values between 0 and 1, or close to 1;")]
    [SerializeField] private Vector2 speedBounds;
    [Tooltip("Expected values between 0.5 and 2;")]
    [SerializeField] private Vector2 intervalBounds;
    [SerializeField] private GameObject diamond1;
    [SerializeField] private GameObject diamond2;

    private class DiamondData {
        public float speed;
        public float sizeX;
        public float sizeY;
    } private DiamondData diamond1data = new();
    private DiamondData diamond2data = new();

    void Start() {
        StartCoroutine(_StarRandomization(diamond1data));
        StartCoroutine(_StarRandomization(diamond2data));
    }

    void Update() {
        diamond1.transform.localScale = Vector2.MoveTowards(diamond1.transform.localScale, 
                                                            new Vector2(diamond1data.sizeX,
                                                            diamond1data.sizeY), Time.deltaTime * diamond1data.speed);
        diamond2.transform.localScale = Vector2.MoveTowards(diamond2.transform.localScale,
                                                            new Vector2(diamond2data.sizeX,
                                                            diamond2data.sizeY), Time.deltaTime * diamond2data.speed);
    }

    private IEnumerator _StarRandomization(DiamondData data) {
        while (true) {
            yield return new WaitForSeconds(Random.Range(intervalBounds.x, intervalBounds.y));
            data.speed = Random.Range(speedBounds.x, speedBounds.y);
            data.sizeX = Mathf.Max(data.sizeY * 0.75f, Random.Range(resizeBounds.x, resizeBounds.y));
            data.sizeY = Mathf.Max(data.sizeX * 0.75f, Random.Range(resizeBounds.x, resizeBounds.y));
        }
    }
}