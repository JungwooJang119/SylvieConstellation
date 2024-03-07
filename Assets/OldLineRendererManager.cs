using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class OldLineRendererManager : MonoBehaviour
{
    private LineRenderer lineRender;

    // Technical
    private Vector2 mousePosition;
    private Vector2 startMousePosition;
    private int numLines;
    private bool drawing;

    // Start is called before the first frame update
    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        lineRender.positionCount = 1;
        numLines = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawingConstellation();
        }
        if (Input.GetMouseButton(0))
        {
            DrawingConstellation();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopDrawingConstellation();
        }

        if (drawing)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Could be changed to new input system, but works
            lineRender.SetPosition(numLines - 2, new Vector3(startMousePosition.x, startMousePosition.y, 0f));
            lineRender.SetPosition(numLines - 1, new Vector3(mousePosition.x, mousePosition.y, 0f));
            lineRender.endColor = Color.white;
            foreach (RaycastHit2D hit in Physics2D.LinecastAll(startMousePosition, mousePosition))
            {
                if (hit != null && hit.collider.tag == "Obstacle")
                {
                    lineRender.endColor = Color.red;
                    return;
                }
            }
            lineRender.endColor = Color.white;
        }     
    }

    public void StartDrawingConstellation()
    {
        startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Could be changed to new input system, but works
        numLines++;
        lineRender.positionCount += 1;
    }

    // Activates drawing logic in update()
    // Input System Performed
    public void DrawingConstellation()
    {
        drawing = true;
    }

    public void StopDrawingConstellation()
    {
        drawing = false;
        lineRender.endColor = Color.white;
        foreach (RaycastHit2D hit in Physics2D.LinecastAll(startMousePosition, mousePosition))
        {
            if (hit != null && hit.collider.tag == "Obstacle")
            {
                numLines--;
                lineRender.positionCount -= 1;
            }
        }
    }

}
