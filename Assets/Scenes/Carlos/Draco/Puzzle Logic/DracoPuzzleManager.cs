using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleManagement;

public class DracoPuzzleManager : MonoBehaviour {

    [SerializeField] private PuzzleID puzzleID;
    [SerializeField] private DracoConstellationBrain puzzleBrain;
    [SerializeField] private float puzzleBeginDelay;

    public GameObject proc;


    private void Awake() {
        puzzleBrain.OnPuzzleEnd += PuzzleBrain_OnPuzzleEnd;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        StartPuzzle();
    }

    private void PuzzleBrain_OnPuzzleEnd(bool success) {
        /// Here goes what happens after the puzzle ends;
        PuzzleManager pm;
        if ((pm = PuzzleManager.Instance) != null) pm.CompletePuzzle(puzzleID);
        StartCoroutine(Completed());
        Debug.Log("You won/lost!");
    }

    public void StartPuzzle() => StartCoroutine(_BeginPuzzle(puzzleBeginDelay));

    private IEnumerator _BeginPuzzle(float delay) {
        yield return new WaitForSeconds(delay);
        puzzleBrain.BeginPuzzle();
    }

    IEnumerator Completed()
    {
        AudioManager.Instance.FadeMusic(true, true);
        NotificationManager.Instance.TestPuzzleCompleteNotification();
        yield return new WaitForSeconds(4f);
        proc.GetComponent<PuzzleProc>().PuzzleInit();
    }

}
