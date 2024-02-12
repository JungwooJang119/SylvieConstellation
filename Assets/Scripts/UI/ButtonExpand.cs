using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class ButtonExpand : MonoBehaviour
{
    //different states the button is in
    enum ButtonState
    {
        REST, GROW, SHRINK
    }

    //speed of button movement
    [SerializeField] float buttonSpeed = 0.02f;
    //% of exspantion
    [SerializeField] float buttonScaler = 1.5f;

    ButtonState buttState;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = Vector3.one * buttonSpeed;

        buttState = ButtonState.REST;
    }

    // Update is called once per frame
    void Update()
    {
        //checks to scale button
        switch (buttState)
        {
            case ButtonState.REST:
            break;

            case ButtonState.GROW:
            transform.localScale += movement;
            break;

            case ButtonState.SHRINK:
            transform.localScale -= movement;
            break;

            default:
            break;
        }

        //stops button from scaling
        if (buttState > ButtonState.REST && (transform.localScale.y <= 1.0f || transform.localScale.y >= buttonScaler))
        {
            buttState = ButtonState.REST;
        }
    }

    //called when pointer enters
    public void ButtonGrow(GameObject button){

        buttState = ButtonState.GROW;
    }

    //called when pointer exits
    public void ButtonShrink(GameObject button){
        buttState = ButtonState.SHRINK;
    }
}
