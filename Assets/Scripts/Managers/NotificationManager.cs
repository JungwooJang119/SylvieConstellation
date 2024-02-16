using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws one of several notification types onto the screen whenever one of a
/// set of events occurs.
/// </summary>
public class NotificationManager : Singleton<NotificationManager>
{
    public AudioClip[] puzzleCompleteSounds;

    public enum NotificationType
    {
        PUZZLE_COMPLETED,
        NEW_AREA,
    }

    public class Notification
    {
        public NotificationType type;
        public string text;
        public AudioClip clip;

        public Notification(NotificationType type, string text, AudioClip clip = null)
        {
            this.type = type;
            this.text = text;
            this.clip = clip;
        }

        public static Notification PuzzleComplete
        {
            get
            {
                AudioClip[] sounds = Instance.puzzleCompleteSounds;
                return new Notification(NotificationType.PUZZLE_COMPLETED, "Puzzle Complete!", sounds[Random.Range(0, sounds.Length)]);
            }
        }
    }

    private void Awake()
    {
        InitializeSingleton(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
