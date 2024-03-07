using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test class. Remove when applicable;
/// </summary>
public class SpawnTextIfComplete : MonoBehaviour {

    [SerializeField] private GameObject text;

    void Awake() {
        GetComponent<PuzzleManagement.PuzzleProc>().OnPuzzleComplete += () => text.SetActive(true);
    }
}
