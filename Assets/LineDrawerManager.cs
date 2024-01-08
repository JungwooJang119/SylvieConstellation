using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineDrawerManager : MonoBehaviour
{

    private LineRenderer lineRender;
    private Vector2 mousePosition;
    private Vector2 startMousePosition;
    private int numLines;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRender= GetComponent<LineRenderer>();
        lineRender.positionCount = 1;
        numLines = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            numLines++;
            lineRender.positionCount += 1;
        }
        if (Input.GetMouseButton(0)) {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRender.SetPosition(numLines - 2, new Vector3(startMousePosition.x, startMousePosition.y, 0f));
            lineRender.SetPosition(numLines - 1, new Vector3(mousePosition.x, mousePosition.y, 0f));
        }
    }
}
