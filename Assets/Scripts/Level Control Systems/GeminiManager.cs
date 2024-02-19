using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeminiManager : MonoBehaviour
{
    [SerializeField] private static Transform[,] constellationArr;
    [SerializeField] GameObject constellationController;
    private static int curPollusR;
    private static int curPollusC;
    private static int curCastorR;
    private static int curCastorC;
    private static int targetPositionr1;
    private static int targetPositionr2;
    private static int targetPositionc1;
    private static int targetPositionc2;
    [SerializeField] GameObject castor;
    [SerializeField] GameObject pollus;
    private static Transform castorPos;
    private static Transform pollusPos;
    
    // Start is called before the first frame update
    void Start()
    {
        constellationArr = new Transform[7,7];
        curPollusR = 3;
        curPollusC = 0;
        curCastorR = 3;
        curCastorC = 6;
        targetPositionr1 = 2;
        targetPositionr2 = 4;
        targetPositionc1 = 2;
        targetPositionc2 = 2;
        castorPos = castor.GetComponent<Transform>();
        pollusPos = pollus.GetComponent<Transform>();
        Debug.Log(castorPos == null);
        Debug.Log(pollusPos == null);
        for (int i = 0; i < constellationController.transform.childCount; i++) {
            int childCount = 0;
            for (int j = 0; j < 7; j++) {
                if (childCount >= constellationController.transform.GetChild(i).childCount) {
                    break;
                }
                int curY = int.Parse(constellationController.transform.GetChild(i).GetChild(childCount).name.Substring(3,1));
                constellationArr[i,curY] = constellationController.transform.GetChild(i).GetChild(childCount);
                //Debug.Log(constellationArr[i, curY].gameObject.name);
                childCount++;
                //worry about highlighting later
            }
        }
        
    }

    public static void move(GameObject button) {
        int r = int.Parse(button.name.Substring(1,1));
        int c = int.Parse(button.name.Substring(3,1));
        Debug.Log("Button at: " + r + ", " + c);
        int castorR = curCastorR;
        int castorC = curCastorC;
        int pollusR = curPollusR;
        int pollusC = curPollusC;

        Debug.Log("Castor at: " + castorR + ", " + castorC);
        Debug.Log("Pollus at: " + pollusR + ", " + pollusC);
        //comparing current position to the button that was pressed

        //castor should just move to the button if possible
        if (r > curCastorR) {
            castorR++;
            pollusR--;
        } else if (r < curCastorR) {
            castorR--;
            pollusR++;
        } 
        if (c > curCastorC) {
            castorC++;
            pollusC--;
        } else if (c < curCastorC) {
            castorC--;
            pollusC++;
        } 

        // //pollus should move AWAY from the button if possible
        // if (r > curPollusR) {
        //     pollusR--;
        // } else if (r < curPollusR) {
        //     pollusR++;
        // } 
        // if (c > curPollusC) {
        //     pollusC--;
        // } else if (c < curPollusC) {
        //     pollusC++;
        // } 

        Debug.Log("Castor wants to be at: " + castorR + ", " + castorC);
        Debug.Log("Pollus wants to be at: " + pollusR + ", " + pollusC);
        //ensure that the new position is available 
        if (castorR < 7 && castorR >= 0 && castorC < 7 && castorC >= 0 && constellationArr[castorR,castorC] != null) {
            castorPos.position = new Vector3(constellationArr[castorR,castorC].position.x, constellationArr[castorR,castorC].position.y,0) ;
            curCastorR = castorR;
            curCastorC = castorC;
        }
        if (pollusR < 7 && pollusR >= 0 && pollusC < 7 && pollusC >= 0 && constellationArr[pollusR,pollusC] != null) {
            pollusPos.position = new Vector3(constellationArr[pollusR,pollusC].position.x, constellationArr[pollusR,pollusC].position.y,0) ;
            curPollusR = pollusR;
            curPollusC = pollusC;
        }
        Debug.Log("Castor at: " + curCastorR + ", " + curCastorC);
        Debug.Log("Pollus at: " + curPollusR + ", " + curPollusC);
        
    }



    //main idea: store the grid in a 2D array. 
        //highlight the buttons adjacent to pollux and castors current position
        //when they click on a button, check to see where they are in relation to the button
        //if the position they would go to is out of bounds, have them do nothing
        //otherwise, have them move towards/away from that button
    //update the highlights
    //if their transform.positions are equal to the target transform.positions, end the sequence
    // Update is called once per frame
    void Update()
    {
        if (curCastorR == targetPositionr1 && curCastorC == targetPositionc1) {
            if (curPollusR == targetPositionr2 && curPollusC == targetPositionc2) {
                Debug.Log("you win!");
            }
        }
        if (curPollusR == targetPositionr1 && curPollusC == targetPositionc1) {
            if (curCastorR == targetPositionr2 && curCastorC == targetPositionc2) {
                Debug.Log("you win!");
            }
        }
    }
}
