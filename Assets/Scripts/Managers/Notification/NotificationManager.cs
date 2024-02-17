using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Draws one of several notification types onto the screen whenever one of a
/// set of events occurs.
/// </summary>
public class NotificationManager : Singleton<NotificationManager>
{
    /// <summary>
    /// The list of sounds from which to be randomly picked for a "Puzzle
    /// Complete" notification.
    /// </summary>
    [SerializeField] private AudioClip[] puzzleCompleteSounds;

    /// <summary>
    /// The AudioSource on which to play the notification sounds.
    /// </summary>
    /// Best to use <see cref="AudioSource.PlayOneShot(AudioClip)"/> rather
    /// than <see cref="AudioSource.Play()"/>.
    [SerializeField] private AudioSource notifAudio;
    /// <summary>
    /// The Animator to trigger when getting a notification. Set the "Show"
    /// trigger to trigger a notification animation.
    /// </summary>
    [SerializeField] private Animator notifAnim;
    /// <summary>
    /// The text object that is animated when a notification is triggered.
    /// </summary>
    [SerializeField] private TextMeshProUGUI notifText;

    /// <summary>
    /// The list of notifications currently waiting to be triggered.
    /// </summary>
    private List<Notification> notifQueue;
    /// <summary>
    /// Whether or not a notification can be freely displayed right now.
    /// </summary>
    [HideInInspector] public bool queueOpen;

    /// <summary>
    /// The different types of notification. Should be different for a 
    /// different event.
    /// </summary>
    public enum NotificationType
    {
        PUZZLE_COMPLETED,
        NEW_AREA,
    }

    /// <summary>
    /// Holds all the data that makes up a notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// The type of event that triggered the notification.
        /// </summary>
        public NotificationType type;
        /// <summary>
        /// The text to show when the notification is triggered.
        /// </summary>
        public string text;
        /// <summary>
        /// The audio clip to play when the notification is triggered.
        /// If null, don't play a sound. This should be supported everywhere.
        /// </summary>
        public AudioClip clip;

        /// <summary>
        /// Create a new notification.
        /// </summary>
        /// <param name="type"><seealso cref="type"/></param>
        /// <param name="text"><seealso cref="text"/></param>
        /// <param name="clip"><seealso cref="clip"/></param>
        public Notification(NotificationType type, string text, AudioClip clip = null)
        {
            this.type = type;
            this.text = text;
            this.clip = clip;
        }

        /// <summary>
        /// Creates a random "puzzle complete" notification using one of the
        /// (possibly multiple) sounds in <see cref="puzzleCompleteSounds"/>.
        /// </summary>
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
        queueOpen = true;
        notifQueue = new List<Notification>();
        InitializeSingleton(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Just a debug measure to test the "puzzle complete" notifications.
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            TestPuzzleCompleteNotification();
        }

        // If there are notifications to be displayed, and one is not currently
        // being displayed, show a notification and dequeue it.
        if (queueOpen && notifQueue.Count > 0)
        {
            ShowNotification(notifQueue[0]);
            notifQueue.RemoveAt(0);
        }
    }

    /// <summary>
    /// Trigger <see cref="notifAudio"/> and <see cref="notifAnim"/> with a
    /// given notification.
    /// </summary>
    /// <param name="notification">The notification to display</param>
    private void ShowNotification(Notification notification)
    {
        if (notification.clip)
        {
            notifAudio.PlayOneShot(notification.clip);

            // Debug message, feel free to delete or comment out
            Debug.Log($"You just heard: {notification.clip.name}");
        }

        notifText.text = notification.text;
        notifAnim.SetTrigger("Show");
        queueOpen = false;
    }

    /// <summary>
    /// Create a random "puzzle complete" notification and show it
    /// </summary>
    private void TestPuzzleCompleteNotification()
    {
        Notification pc = Notification.PuzzleComplete;
        notifQueue.Add(pc);
    }
}
