using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using System.Threading;

public class InteractWithAutosaveZone : MonoBehaviour
{
    float autosaveTimer;

    // Start is called before the first frame update
    void Start()
    {
        autosaveTimer = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        autosaveTimer -= Time.unscaledDeltaTime;
        if (Keyboard.current.digit9Key.wasPressedThisFrame)
        {
            Save();
        }
        if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            Load();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("AutosaveZone"))
        {
            if (autosaveTimer < 0)
            {
                Save();
                // If autosave occurs, wait 45 seconds minimum until another one
                IncrementAutosaveTimerTo(45f);
            }
            // If touching a trigger, wait 15 seconds minimum until an autosave (for
            // when the player is AFK at a trigger)
            IncrementAutosaveTimerTo(15f);
        }
    }

    void IncrementAutosaveTimerTo(float upTo)
    {
        autosaveTimer = Mathf.Max(autosaveTimer, upTo);
    }

    void Save()
    {
        SaveSystem.Save save = SaveSystem.GenerateSave();
        Task.Run(() => SaveSystem.SaveToFile(save));
    }

    void Load()
    {
        SaveSystem.LoadSave(SaveSystem.LoadFromFile(SaveSystem.DUMMY_FILE_NAME));
    }
}
