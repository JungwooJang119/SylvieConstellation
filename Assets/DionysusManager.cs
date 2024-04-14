using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleManagement;

public class DionysusManager : MonoBehaviour
{

    public GameObject proc;
    public GameObject Drunk;

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
        Drunk.GetComponent<DrunkGoggles>().SetDrunkIntensity(3);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(Completed());
        }
    }

    IEnumerator Completed()
    {
        AudioManager.Instance.FadeMusic(true, true);
        NotificationManager.Instance.TestPuzzleCompleteNotification();
        yield return new WaitForSeconds(4f);
        Drunk.GetComponent<DrunkGoggles>().SetDrunkIntensity(0);
        proc.GetComponent<PuzzleProc>().PuzzleInit();
    }
}
