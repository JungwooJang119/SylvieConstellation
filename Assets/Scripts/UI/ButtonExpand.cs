using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class ButtonExpand : MonoBehaviour
{

    //speed of button movement
    [SerializeField] float buttonSpeed = 0.02f;
    //% of exspantion
    [SerializeField] float buttonScaler = 1.5f;

    Vector3 movement;



    // Start is called before the first frame update
    void Start()
    {
        movement = Vector3.one * buttonSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //called when pointer enters
    public void ButtonGrow(GameObject button){
        StopAllCoroutines();
        StartCoroutine(Grow(true));;
    }

    //called when pointer exits
    public void ButtonShrink(GameObject button){
        StopAllCoroutines();
        StartCoroutine(Grow(false));;
    }

    //coroutine handeling button scaling
    public IEnumerator Grow(bool growing) {
        if (growing)
        {
            while (transform.localScale.y < buttonScaler)
            {
                transform.localScale += movement;
                yield return null;
            }

        } else {
            while (transform.localScale.y > 1.0f)
            {
                transform.localScale -= movement;
                yield return null;
            }

        }
    }
}
