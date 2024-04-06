using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TargetIndicator : MonoBehaviour
{
    public Vector2 target;
    public GameObject pointer;
    public float HideDistance;
    private Image image;
    void Start() {
        target = TransitionManager.Instance.targetPosition;
        image = pointer.GetComponent<Image>();
    }

    void Update()
    {
        target = TransitionManager.Instance.targetPosition;
        var dir = target - (Vector2)transform.position;

        if (dir.magnitude <= HideDistance && image.color.a > 0.05)
        {
            StartCoroutine(FadeImage(true));
        }
        else if (dir.magnitude > HideDistance && image.color.a <= 0.01)
        {
            StartCoroutine(FadeImage(false));
        }

        var angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        pointer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}

