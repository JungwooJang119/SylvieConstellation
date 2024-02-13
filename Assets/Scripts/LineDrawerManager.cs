using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the line-drawing mechanic for the constellation puzzle. Mouse clicks
/// create a new vertex when possible (unless colliding with an object). Interacts
/// with the <see cref="ConstellationLines"/> script to draw lines.
/// </summary>
/// Original authors: Jung and Zation
/// Edited by Aiden for compatibility with <see cref="ConstellationLines"/>
public class LineDrawerManager : MonoBehaviour
{
    /// <summary>
    /// The prefab of the constellation line itself <seealso cref="ConstellationLines"/>
    /// </summary>
    [SerializeField] private GameObject linePrefab;

    /// <summary>
    /// The position of the initial point of the current line.
    /// </summary>
    private Vector2 startMousePosition;
    /// <summary>
    /// Whether or not a line is being drawn
    /// </summary>
    private bool drawing;
    /// <summary>
    /// The script of the current line being drawn.
    /// </summary>
    private ConstellationLines currentLine;
    /// <summary>
    /// The chosen inner colors for the line when able/unable to create a new line.
    /// </summary>
    [SerializeField] private Color regularInsideColor, badInsideColor;
    /// <summary>
    /// The "inverse size" <seealso cref="ConstellationLines.inverseSize"/> of
    /// the constellation line in various situations, since it changes dynamically
    /// during gameplay.
    /// </summary>
    private const float 
        DRAWING_LINE = 6.5f, 
        CANNOT_COMPLETE = 10f, 
        FINISHED = 4.7f;

    // Start is called before the first frame update
    void Start()
    {
        drawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartDrawingConstellation();
        }
        
        if (drawing)
        {
            DrawingConstellation();
        }
    }

    /// <summary>
    /// If possible, create a new line at the cursor position.
    /// </summary>
    public void StartDrawingConstellation()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

        if (drawing)
        {
            // If already drawing a line, check to see if the line is not occluded
            // before drawing a new one.
            currentLine.point2 = mousePosition;
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
            // If not drawing a line, check that the new line will not be occluded.
            if (Physics2D.OverlapPoint(mousePosition))
            {
                return;
            }
        }
        startMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        if (drawing)
        {
            // Change the width of the line being drawn currently
            currentLine.inverseSize = FINISHED;
        }
        else
        {
            drawing = true;
        }
        // Create a new constellation line and make it the current line
        currentLine = Instantiate(linePrefab, Camera.main.transform.position + Vector3.forward * 10, Quaternion.identity)
            .GetComponent<ConstellationLines>();
        currentLine.point1 = startMousePosition;
    }

    /// <summary>
    /// Set the end position of the current line to the current mouse position,
    /// and check whether the line is occluded.
    /// </summary>
    public void DrawingConstellation()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

        // Set the color and size of the line to indicate whether it is occluded
        currentLine.inverseSize = DRAWING_LINE;
        currentLine.color = regularInsideColor;
        currentLine.point2 = mousePosition;
        foreach (RaycastHit2D hit in Physics2D.LinecastAll(startMousePosition, mousePosition))
        {
            if (hit && hit.collider.CompareTag("Obstacle"))
            {
                currentLine.inverseSize = CANNOT_COMPLETE;
                currentLine.color = badInsideColor;
                break;
            }
        }
    }
}
