using UnityEngine;

namespace PuzzleManagement {

    /// <summary>
    /// Sample component to Initiate a puzzle accounting for completion;
    /// </summary>
    public class PuzzleProc : MonoBehaviour {

        public event System.Action OnPuzzleComplete;

        [SerializeField] private PuzzleID puzzleID;

        public void PuzzleInit() {
            bool complete = PuzzleManager.Instance.GetPuzzleStatus(puzzleID);
            if (complete) {
                OnPuzzleComplete?.Invoke();
            } else {
                TransitionManager.Instance.GoToScene((int) puzzleID);
            }
        }
    }
}
