using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LineDrawerManager : MonoBehaviour
{
    // Prefabs
    [SerializeField] private GameObject linePrefab;

    // Technical
    private Vector2 startMousePosition;
    private bool drawing;
    private ConstellationLines currentLine;
    [SerializeField] private Color regularInsideColor, badInsideColor;

    // Start is called before the first frame update
    void Start()
    {
        drawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Legacy Input
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartDrawingConstellation();
        }
        
        // Drawing Logic, didn't encapsulate to another method to mitagate call stack performance.
        if (drawing)
        {
            DrawingConstellation();
        }
    }

    // Creates a point where the constellation will be drawn
    // Input System Started
    public void StartDrawingConstellation()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        if (drawing)
        {
            foreach (RaycastHit2D hit in Physics2D.LinecastAll(startMousePosition, mousePosition))
            {
                if (hit && hit.collider.CompareTag("Obstacle"))
                {
                    return;
                }
            }
        }
        else
        {
            if (Physics2D.OverlapPoint(mousePosition))
            {
                return;
            }
        }
        startMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        if (drawing)
        {
            currentLine.inverseSize = 4.7f;
        }
        else
        {
            drawing = true;
        }
        currentLine = Instantiate(linePrefab, Camera.main.transform.position + Vector3.forward * 10, Quaternion.identity)
            .GetComponent<ConstellationLines>();
        currentLine.point1 = startMousePosition;
    }

    // Activates drawing logic in update()
    // Input System Performed
    public void DrawingConstellation()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        currentLine.point2 = mousePosition;
        currentLine.inverseSize = 6.5f;
        currentLine.color = regularInsideColor;
        foreach (RaycastHit2D hit in Physics2D.LinecastAll(startMousePosition, mousePosition))
        {
            if (hit && hit.collider.CompareTag("Obstacle"))
            {
                currentLine.inverseSize = 10f;
                currentLine.color = badInsideColor;
                break;
            }
        }
    }

    // Deactivates drawing logic in update and checks for collisions
    // Input System Released
    public void StopDrawingConstellation()
    {
        drawing = false;
    }
}
