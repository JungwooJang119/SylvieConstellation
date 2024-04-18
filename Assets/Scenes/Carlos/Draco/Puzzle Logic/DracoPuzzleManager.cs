using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleManagement;

public class DracoPuzzleManager : MonoBehaviour {

    [SerializeField] private PuzzleID puzzleID;
    [SerializeField] private DracoConstellationBrain puzzleBrain;
    [SerializeField] private float puzzleBeginDelay;

    private void Awake() {
        puzzleBrain.OnPuzzleEnd += PuzzleBrain_OnPuzzleEnd;
    }

    private void PuzzleBrain_OnPuzzleEnd(bool success) {
        /// Here goes what happens after the puzzle ends;
        PuzzleManager pm;
        if ((pm = PuzzleManager.Instance) != null) pm.CompletePuzzle(puzzleID);

        Debug.Log("You won/lost!");
    }

    public void StartPuzzle() => StartCoroutine(_BeginPuzzle(puzzleBeginDelay));

    private IEnumerator _BeginPuzzle(float delay) {
        yield return new WaitForSeconds(delay);
        puzzleBrain.BeginPuzzle();
    }
}
