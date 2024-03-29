using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitedAreaManager : Singleton<VisitedAreaManager>
{
    [HideInInspector] public static HashSet<string> visitedAreas;

    private void Awake()
    {
        InitializeSingleton(gameObject);
        visitedAreas = new HashSet<string>();
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
