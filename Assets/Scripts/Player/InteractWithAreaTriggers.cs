using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithAreaTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AreaTrigger"))
        {
            AreaTrigger at = collision.GetComponent<AreaTrigger>();
            string areaName = at.areaName;

            bool areaIsNew = !VisitedAreaManager.AreaIsVisited(areaName);

            if (areaIsNew)
            {
                VisitedAreaManager.visitedAreas.Add(areaName);
            }

            NotificationManager.Notification notif = areaIsNew
                ? NotificationManager.Notification.NewArea(areaName)
                : NotificationManager.Notification.NowEntering(areaName);
            NotificationManager.AddToQueue(notif);
        }
    }
}
