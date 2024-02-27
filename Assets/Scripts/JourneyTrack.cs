using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity.Tests;

public class JourneyTrack
{
   [SerializeField]
    /**Create a List of Vector3's to hold the positions of constellations Sylvie interacted with**/
    private List<Vector3> journey = new List<Vector3>();

    [SerializeField]
    private EditedLineDrawerManager endingLines;

    /**Method to add locations to the journey vector**/
    public void addLocation(Vector3 position) {
        journey.Add(position); 

        // if (journey.Count == 4) {
        //     endingLines = GameObject.Find("LineRenderer").GetComponent<EditedLineDrawerManager>();
        //     endingLines.journeyDrawAction(journey);
        // }
    }

    /**Getter for Journey list**/
    public List<Vector3> getJourney() {
        return journey;
    }
}
