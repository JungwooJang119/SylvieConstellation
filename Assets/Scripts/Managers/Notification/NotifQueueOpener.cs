using UnityEngine;

/// <summary>
/// Tells the <see cref="NotificationManager"/> that the current notification
/// has finished displaying.
/// </summary>
public class NotifQueueOpener : MonoBehaviour
{
    /// <summary>
    /// Speaks to the current instance of <see cref="NotificationManager"/> and
    /// tells it to open up the notification queue.
    /// </summary>
    public void OpenQueue()
    {
        NotificationManager.Instance.queueOpen = true;
    }
}
