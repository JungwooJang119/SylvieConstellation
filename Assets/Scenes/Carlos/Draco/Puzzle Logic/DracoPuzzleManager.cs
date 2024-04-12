using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DracoPuzzleManager : MonoBehaviour {

    [SerializeField] private DracoConstellationBrain puzzleBrain;
    [SerializeField] private float puzzleBeginDelay;

    private void Awake() {
        puzzleBrain.OnPuzzleEnd += PuzzleBrain_OnPuzzleEnd;
    }

    private void PuzzleBrain_OnPuzzleEnd(bool success) {
        /// Here goes what happens after the puzzle ends;
    }

    public void StartPuzzle() => StartCoroutine(_BeginPuzzle(puzzleBeginDelay));

    private IEnumerator _BeginPuzzle(float delay) {
        yield return new WaitForSeconds(delay);
        puzzleBrain.BeginPuzzle();
    }
}
