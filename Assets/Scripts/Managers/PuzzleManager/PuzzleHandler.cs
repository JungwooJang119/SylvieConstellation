using UnityEngine;

namespace PuzzleManagement {

    /// <summary>
    /// Base class for Puzzle Logic scripts;
    /// Enforces two lines to help announce puzzle completion;
    /// </summary>
    public abstract class PuzzleHandler : MonoBehaviour {

        [SerializeField] protected PuzzleID puzzleID;
        public virtual void FinalizePuzzle() => PuzzleManager.Instance.CompletePuzzle(puzzleID);
    }
}
