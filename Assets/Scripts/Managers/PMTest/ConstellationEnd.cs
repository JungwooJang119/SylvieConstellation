using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleManagement;

/// <summary>
/// Test class. Remove when applicable;
/// </summary>
public class ConstellationEnd : PuzzleHandler {

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            FinalizePuzzle();
            TransitionManager.Instance.GoToScene(0);
        }
    }
}
