using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartialText : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    public float textProportion;

    public string text;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int chars = (int)(textProportion * text.Length);
        tmp.text = text[0..chars];
    }
}
