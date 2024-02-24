using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Image blackOutSquare;

    // Update is called once per frame
    void Update()
    {
        //Press F to make the screen fade to black then return. Can be changed in the future as needed.
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(FadeThroughBlack(1, 1f));;
        }
    }

    private IEnumerator FadeThroughBlack(int midDelay, float fadeTime)
    {
        yield return FadeTo(Color.black, fadeTime);
        yield return new WaitForSeconds(midDelay);
        yield return FadeTo(Color.clear, fadeTime);
    }

    private IEnumerator FadeTo(Color destination, float fadeTime)
    {
        Color originalColor = blackOutSquare.color;

        for (float t = 0.01f; t < fadeTime; t += Time.deltaTime)
        {
            blackOutSquare.color = Color.Lerp(originalColor, destination, t / fadeTime);
            yield return null;
        }

        blackOutSquare.color = destination;
    }
}
