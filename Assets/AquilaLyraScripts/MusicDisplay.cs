using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDisplay : MonoBehaviour
{
    [SerializeField] GameObject display;
    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("collision!");
        if (col.gameObject.tag == "Player") {
            display.SetActive(true);
        }
    }

    void OnCollisionExist2D(Collision2D col) {
        Debug.Log("exit!");
        if (col.gameObject.tag =="Player") {
            display.SetActive(false);
        }
    }

}
