using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleManagement {

    /// <summary> Enumeration associating each puzzle with its corresponding scene; 
    /// <br></br> Each integer value corresponds to a scene build index
    /// <br></br> i.e. 'Lovers' is set to 1, and thus points at scene index 1; </summary>
    public enum PuzzleID {
        GentleLady = 1,
        HotDude = 2,
        AnotherHotDude = 3,
        DragonHotDude = 4,
        ImMeltingDude = 5,
    }

    /// <summary>
    /// Singleton class. Keeps track of puzzle completion;
    /// </summary>
    public class PuzzleManager : MonoBehaviour {

        public static PuzzleManager Instance { get; private set; }

        /// <summary> How many puzzles have been completed; </summary>
        private int PuzzleProgress => PuzzleProgressMap.Select(kvp => kvp.Value > 0).Count();
        /// <summary> Maps each puzzle to the order in which it was completed;
        /// <br></br> <i> 0 → Incomplete // 1+ → Complete; </i> </summary>
        public Dictionary<PuzzleID, int> PuzzleProgressMap { get; private set; }

        void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else Destroy(gameObject);

            PuzzleProgressMap = new();
            foreach (PuzzleID puzzleID in System.Enum.GetValues(typeof(PuzzleID))) {
                PuzzleProgressMap[puzzleID] = 0;
            }
        }

        /// <summary>
        /// Whether the given puzzle has been completed;
        /// </summary>
        /// <param name="puzzleID"> ID of the puzzle to check for completion; </param>
        /// <returns> True if the puzzle was completed, false otherwise; </returns>
        public bool GetPuzzleStatus(PuzzleID puzzleID) => PuzzleProgressMap[puzzleID] > 0;

        /// <summary>
        /// Sets the completion order of the given puzzle;
        /// </summary>
        /// <param name="puzzleID"> Enum associated with the puzzle to mark as complete; </param>
        public void CompletePuzzle(PuzzleID puzzleID) => PuzzleProgressMap[puzzleID] = PuzzleProgress + 1;
    }
}