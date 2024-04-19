using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleManagement;

public class DionysusManager : MonoBehaviour
{

    public GameObject proc;
    public GameObject Drunk;
    public GameObject bottle1;
    public GameObject bottle2;
    public GameObject bottle3;

    public bool hasCollectedBottle1;
    public bool hasCollectedBottle2;
    public bool hasCollectedBottle3;

    private bool hasDone;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Drunk.GetComponent<DrunkGoggles>().SetDrunkIntensity(1);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (bottle1 == null) {
            hasCollectedBottle1 = true;
        }
        if (bottle2 == null) {
            hasCollectedBottle2 = true;
        }
        if (bottle3 == null) {
            hasCollectedBottle3 = true;
        }
        if (!hasDone && hasCollectedBottle1 && hasCollectedBottle2 && hasCollectedBottle3) {
            hasDone = true;
            StartCoroutine(Completed());
        }
    }

    IEnumerator Completed()
    {
        AudioManager.Instance.FadeMusic(true, true);
        NotificationManager.Instance.TestPuzzleCompleteNotification();
        Drunk.GetComponent<DrunkGoggles>().SetDrunkIntensity(0);
        yield return new WaitForSeconds(4f);
        proc.GetComponent<PuzzleProc>().PuzzleInit();
    }
}
