using UnityEngine;

/// <summary>
/// A handler for placing constellation lines that controls the relevant shader settings.
/// <seealso cref="LineDrawerManager"/>
/// </summary>
public class ConstellationLines : MonoBehaviour
{
    /// <summary>
    /// The two points used to determine the bounds of the line
    /// </summary>
    [HideInInspector] public Vector2 point1, point2;
    /// <summary>
    /// The larger this value is, the narrower the line will appear.
    /// </summary>
    [HideInInspector] public float inverseSize;
    /// <summary>
    /// The inner color of the line
    /// </summary>
    [HideInInspector] public Color color;
    /// <summary>
    /// The renderer for the line
    /// </summary>
    private SpriteRenderer spr;
    /// <summary>
    /// Used to set the shader settings on the line without causing any global
    /// material changes (other tech artists, take note!)
    /// </summary>
    private MaterialPropertyBlock mpb;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        mpb = new MaterialPropertyBlock();
        spr.GetPropertyBlock(mpb);
        spr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the point locations, inverse size, and inner color of the line
        mpb.SetVector("_Point1", point1);
        mpb.SetVector("_Point2", point2);
        mpb.SetFloat("_DistMult", inverseSize);
        mpb.SetColor("_InsideColor", color);
        // Apply the changed shader settings and show the material
        spr.SetPropertyBlock(mpb);
        spr.enabled = true;
    }
}
