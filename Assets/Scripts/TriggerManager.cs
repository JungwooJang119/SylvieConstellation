using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 position = new Vector3();
    private JourneyTrack journey;

    // Start is called before the first frame update
    void Start()
    {
        journey = new JourneyTrack();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**Get child object that was triggered and get its position and then add to journey list**/
    public void whichChild(GameObject child) {
        position = child.transform.position;
        journey.addLocation(position);
    }
}
