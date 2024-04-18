using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitedAreaManager : Singleton<VisitedAreaManager>
{
    /// <summary>
    /// Don't use this to detect if an area has been visited. Instead, use
    /// <see cref="AreaIsVisited(string)"/>.
    /// </summary>
    [HideInInspector] public static HashSet<string> visitedAreas;

    private void Awake()
    {
        InitializeSingleton(gameObject);
    }

    private void Start()
    {
        visitedAreas = new HashSet<string>();
    }

    public static bool AreaIsVisited(string area)
    {
        visitedAreas ??= new HashSet<string>();
        return visitedAreas.Contains(area);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
