using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A script for the player character which enables it to collide 
/// with autosave zones and interact with the save system.
/// </summary>
/// This class should only be on the player character, and it 
/// shouldn't be present when playing a constellation.
public class InteractWithAutosaveZone : MonoBehaviour
{
    // The time until the next autosave (when an autosave zone is touched)
    private float autosaveTimer;

    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.TryLoadGame();
        autosaveTimer = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        autosaveTimer -= Time.unscaledDeltaTime;

        // Debug functionality for saving and loading
        if (Keyboard.current.digit9Key.wasPressedThisFrame)
        {
            SaveSystem.SaveGame();
        }
        if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            SaveSystem.TryLoadGame();
        }

        // If the autosave timer gets low enough, but the player
        // hasn't touched an autosave trigger, save anyway
        if (autosaveTimer < -300f)
        {
            SaveSystem.SaveGame();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("AutosaveZone"))
        {
            if (autosaveTimer < 0)
            {
                SaveSystem.SaveGame();
                // If autosave occurs, wait 45 seconds minimum until another one
                IncrementAutosaveTimerTo(45f);
            }
            // If touching a trigger, wait 15 seconds minimum until an autosave (for
            // when the player is AFK at a trigger)
            IncrementAutosaveTimerTo(15f);
        }
    }

    private void OnApplicationQuit()
    {
        // Save the game before quitting
        SaveSystem.SaveGame();
    }

    /// <summary>
    /// Increases the autosave timer up to this value.
    /// </summary>
    /// <param name="upTo">The new minimum value for the autosave timer</param>
    private void IncrementAutosaveTimerTo(float upTo)
    {
        autosaveTimer = Mathf.Max(autosaveTimer, upTo);
    }
}
