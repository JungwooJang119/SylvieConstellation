using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class LineDrawerManager : MonoBehaviour
{
    // Components
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
        // Legacy Input
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
        
        // Drawing Logic, didn't encapsulate to another method to mitagate call stack performance.
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

    // Creates a point where the constellation will be drawn
    // Input System Started
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

    // Deactivates drawing logic in update and checks for collisions
    // Input System Released
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
