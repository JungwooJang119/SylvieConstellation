using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMoveCount : MonoBehaviour
{
    public Text textBox;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count = GeminiManager.moveCount;
        textBox.text = "Move Count: " + count;
    }
}
