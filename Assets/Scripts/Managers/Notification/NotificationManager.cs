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
    /// A sound effect that can be used as a placeholder (or as an actual SFX
    /// if Jung likes it. I just made it in like 2 minutes)
    /// </summary>
    [SerializeField] private AudioClip miscSfx;

    /// <summary>
    /// The AudioSource on which to play the notification sounds.
    /// </summary>
    /// Best to use <see cref="AudioSource.PlayOneShot(AudioClip)"/> rather
    /// than <see cref="AudioSource.Play()"/>.
    [SerializeField] private AudioSource notifAudio;
    /// <summary>
    /// The Animator to trigger when getting a notification.
    /// Set the "Show" trigger to trigger a notification animation.
    /// </summary>
    [SerializeField] private Animator notifAnim;
    /// <summary>
    /// The text object that is animated when a notification is triggered and
    /// the <see cref="Notification.style"/> is <see cref="Style.HEADER"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI headerText;
    /// <summary>
    /// The text object that is animated when a notification is triggered and
    /// the <see cref="Notification.style"/> is <see cref="Style.FOOTER"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI footerText;
    /// <summary>
    /// The text object that is animated for the title of the notification
    /// when a notification is triggered and the <see cref="Notification.style"/>
    /// is <see cref="Style.SIDEBAR"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI sidebarText;
    /// <summary>
    /// The text object that is animated for the description of the notification
    /// when a notification is triggered and the <see cref="Notification.style"/>
    /// is <see cref="Style.SIDEBAR"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI sidebarDescText;

    /// <summary>
    /// The list of notifications currently waiting to be triggered.
    /// </summary>
    private List<Notification> notifQueue;
    /// <summary>
    /// Whether or not a notification can be freely displayed right now.
    /// </summary>
    [HideInInspector] public bool queueOpen;

    public enum Style
    {
        HEADER,
        FOOTER,
        SIDEBAR,
    }

    /// <summary>
    /// Holds all the data that makes up a notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// The text to show when the notification is triggered.
        /// </summary>
        public string text;
        /// <summary>
        /// The description text to show when a notification with the
        /// <see cref="style"/> <see cref="Style.SIDEBAR"/> is triggered.
        /// </summary>
        public string description;
        /// <summary>
        /// The audio clip to play when the notification is triggered.
        /// If null, don't play a sound. This should be supported everywhere.
        /// </summary>
        public AudioClip clip;
        /// <summary>
        /// The volume at which to play <see cref="clip"/>.
        /// </summary>
        public float volume;
        /// <summary>
        /// The manner by which the notification is displayed to the user.
        /// </summary>
        public Style style;

        /// <summary>
        /// Create a new notification.
        /// </summary>
        /// <param name="text"><seealso cref="text"/></param>
        /// <param name="clip"><seealso cref="clip"/></param>
        public Notification(string text, Style style, AudioClip clip = null, float volume = 1f, string description = "")
        {
            this.text = text;
            this.style = style;
            this.clip = clip;
            this.volume = volume;
            this.description = description;
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
                return new Notification("Puzzle Complete!",
                    Style.SIDEBAR,
                    sounds[Random.Range(0, sounds.Length)],
                    description: "You completed the puzzle.");
            }
        }

        public static Notification NewArea(string areaName)
        {
            return new Notification($"Discovered {areaName}!",
                Style.HEADER,
                Instance.miscSfx);
        }

        public static Notification NowEntering(string areaName)
        {
            return new Notification($"Now Entering {areaName}",
                Style.FOOTER,
                Instance.miscSfx,
                volume: 0.4f);
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

    public static void AddToQueue(Notification notification)
    {
        Instance.notifQueue.Add(notification);
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
            notifAudio.PlayOneShot(notification.clip, notification.volume);

            // Debug message, feel free to delete or comment out
            Debug.Log($"You just heard: {notification.clip.name}");
        }

        switch (notification.style)
        {
            case Style.HEADER:
                headerText.text = notification.text;
                notifAnim.SetTrigger("ShowHeader");
                break;
            case Style.FOOTER:
                footerText.text = notification.text;
                notifAnim.SetTrigger("ShowFooter");
                break;
            case Style.SIDEBAR:
                sidebarText.text = notification.text;
                sidebarDescText.text = notification.description;
                notifAnim.SetTrigger("ShowSidebar");
                break;
        }
        queueOpen = false;
    }

    /// <summary>
    /// Create a random "puzzle complete" notification and show it
    /// </summary>
    public void TestPuzzleCompleteNotification()
    {
        Notification pc = Notification.PuzzleComplete;
        notifQueue.Add(pc);
    }
}
