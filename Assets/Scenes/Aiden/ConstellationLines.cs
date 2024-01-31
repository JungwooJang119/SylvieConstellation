using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationLines : MonoBehaviour
{
    [HideInInspector] public Vector2 point1, point2;
    [HideInInspector] public float inverseSize;
    [HideInInspector] public Color color;
    private SpriteRenderer spr;
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
        mpb.SetVector("_Point1", point1);
        mpb.SetVector("_Point2", point2);
        mpb.SetFloat("_DistMult", inverseSize);
        mpb.SetColor("_InsideColor", color);
        spr.SetPropertyBlock(mpb);
        spr.enabled = true;
    }
}
