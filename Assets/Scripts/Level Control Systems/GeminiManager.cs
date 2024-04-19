using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PuzzleManagement;

public class GeminiManager : MonoBehaviour
{
    public GameObject proc;
    private bool hasDone;
    public static int moveCount;


    [SerializeField] private static Transform[,] constellationArr;
    [SerializeField] GameObject constellationController;
    private static int curPollusR;
    private static int curPollusC;
    private static int curCastorR;
    private static int curCastorC;
    private static int curSylvieC;
    private static int curSylvieR;
    private static int prevPollusR;
    private static int prevPollusC;
    private static int prevCastorR;
    private static int prevCastorC;
    private static int prevSylvieR;
    private static int prevSylvieC;
    private static int targetPositionr1;
    private static int targetPositionr2;
    private static int targetPositionc1;
    private static int targetPositionc2;
    [SerializeField] GameObject castor;
    [SerializeField] GameObject pollus;
    [SerializeField] GameObject sylvie;
    private static Transform castorPos;
    private static Transform pollusPos;
    private static Transform sylviePos;
    
    // Start is called before the first frame update
    void Start()
    {
        //starting positions and initializations
        constellationArr = new Transform[7,7];
        curPollusR = 3;
        curPollusC = 0;
        curCastorR = 3;
        curCastorC = 6;
        curSylvieR = 3;
        curSylvieC = 2;
        targetPositionr1 = 2;
        targetPositionr2 = 4;
        targetPositionc1 = 2;
        targetPositionc2 = 2;
        castorPos = castor.GetComponent<Transform>();
        pollusPos = pollus.GetComponent<Transform>();
        sylviePos = sylvie.GetComponent<Transform>();

        Debug.Log("hello");
        //add every star to the array
        for (int i = 0; i < constellationController.transform.childCount; i++) {
            int childCount = 0;
            for (int j = 0; j < 7; j++) {
                //extra childCount variable to account for gaps
                if (childCount >= constellationController.transform.GetChild(i).childCount) {
                    Debug.Log("broke");
                    break;
                }
                //add the child to the array
                int curY = int.Parse(constellationController.transform.GetChild(i).GetChild(childCount).name.Substring(3,1));
                constellationArr[i,curY] = constellationController.transform.GetChild(i).GetChild(childCount);
                childCount++;
                Debug.Log("child");
                //button disabling
                if (!((i == curSylvieR && (curY == curSylvieC - 1 ||curY == curSylvieC + 1))
                    || (curY == curSylvieC && (i == curSylvieR - 1 ||i == curSylvieR + 1)))) {
                    constellationArr[i,curY].gameObject.GetComponent<Button>().interactable = false ;
                    Debug.Log("disable");
                }
            }
        }
        moveCount = 0;
        
    }

    public static void move(GameObject button) {
        //comparsion variables
        int r = int.Parse(button.name.Substring(1,1));
        int c = int.Parse(button.name.Substring(3,1));
        int sylvieR = r;
        int sylvieC = c;
        int castorR = curCastorR;
        int castorC = curCastorC;
        int pollusR = curPollusR;
        int pollusC = curPollusC;
        
        // Debug.Log("Castor at: " + castorR + ", " + castorC);
        // Debug.Log("Pollus at: " + pollusR + ", " + pollusC);
        //comparing current position to the button that was pressed

        //castor should just move to the button if possible
        if (r > curSylvieR) {
            castorR++;
            pollusR--;
        } else if (r < curSylvieR) {
            castorR--;
            pollusR++;
        } 
        if (c > curSylvieC) {
            castorC++;
            pollusC--;
        } else if (c < curSylvieC) {
            castorC--;
            pollusC++;
        } 

        if ((sylvieC == curPollusC && sylvieR == curPollusR) || (sylvieC == pollusC && sylvieR == pollusR)) {
            return;
        }
        if (sylvieR == curCastorR && sylvieC == curCastorC) {
            if (castorR >= 7 || castorR < 0 || castorC >= 7 || castorC < 0) {
                return;
            } else if (constellationArr[castorR, castorC] == null) {
                return;
            }
        }
        // //syvlie/NPC overlap ?
        // if ((sylvieC == curCastorC && sylvieR == curCastorR) || (sylvieC == curPollusC && sylvieR == curPollusR)) {
        //     //bounce animation?
        //     return;
        // }
        // if ((sylvieC == castorC && sylvieR == castorR) || (sylvieC == pollusC && sylvieR == pollusR)) {
        //     //bounce animation?
        //     return;
        // }

        //update sylvie's position
        sylviePos.position = new Vector3(constellationArr[sylvieR,sylvieC].position.x, constellationArr[sylvieR,sylvieC].position.y,0);

        //enable new adjacent buttons
        if (sylvieC + 1 < 7 && constellationArr[sylvieR, sylvieC + 1] != null) {
            constellationArr[sylvieR,sylvieC + 1].gameObject.GetComponent<Button>().interactable = true ;
        }
        if (sylvieC - 1 >= 0 && constellationArr[sylvieR, sylvieC - 1] != null) {
            constellationArr[sylvieR,sylvieC - 1].gameObject.GetComponent<Button>().interactable = true;
        }
        if (sylvieR + 1 < 7 && constellationArr[sylvieR + 1, sylvieC] != null) {
        constellationArr[sylvieR + 1,sylvieC].gameObject.GetComponent<Button>().interactable = true;
        }
        if (sylvieR - 1 >= 0 && constellationArr[sylvieR - 1, sylvieC] != null) {
           constellationArr[sylvieR - 1,sylvieC].gameObject.GetComponent<Button>().interactable = true ;
        }

        //disable old adjacent buttons
        if (curSylvieC + 1 < 7 && constellationArr[curSylvieR, curSylvieC + 1] != null) {
            constellationArr[curSylvieR, curSylvieC + 1].gameObject.GetComponent<Button>().interactable = false ;
        }
        if (curSylvieC - 1 >= 0 && constellationArr[curSylvieR, curSylvieC - 1] != null) {
            constellationArr[curSylvieR,curSylvieC - 1].gameObject.GetComponent<Button>().interactable = false ;
        }
        if (curSylvieR + 1 < 7 && constellationArr[curSylvieR  + 1, curSylvieC] != null) {
            constellationArr[curSylvieR + 1,curSylvieC].gameObject.GetComponent<Button>().interactable = false ;
        }
        if (curSylvieR - 1 >= 0 && constellationArr[curSylvieR - 1, curSylvieC] != null) {
            constellationArr[curSylvieR - 1,curSylvieC].gameObject.GetComponent<Button>().interactable = false ;
        }
        //update the saved position
        prevSylvieR = curSylvieR;
        prevSylvieC = curSylvieC;
        curSylvieR = sylvieR;
        curSylvieC = sylvieC;
        
        //collision case
        if ((pollusC == curCastorC && pollusR == curCastorR) && (castorC == curPollusC && castorR == curPollusR)) {
            //bounce animation?
            return;
        }
         if (pollusC == castorC && pollusR == castorR) {
            //bounce animation?
            return;
        }
        
        //update Pollus' position
        if (pollusR < 7 && pollusR >= 0 && pollusC < 7 && pollusC >= 0 && constellationArr[pollusR,pollusC] != null) {
            pollusPos.position = new Vector3(constellationArr[pollusR,pollusC].position.x, constellationArr[pollusR,pollusC].position.y,0) ;
            prevPollusR = curPollusR;
            prevPollusC = curPollusC;
            curPollusR = pollusR;
            curPollusC = pollusC;
        }

        //update Castor's position
        if (castorR < 7 && castorR >= 0 && castorC < 7 && castorC >= 0 && constellationArr[castorR, castorC] != null) {
            castorPos.position = new Vector3(constellationArr[castorR,castorC].position.x, constellationArr[castorR,castorC].position.y,0) ;
            prevCastorR = curCastorR;
            prevCastorC = curCastorC;
            curCastorR = castorR;
            curCastorC = castorC;
        }
        IncrementMoveCount();
        
    }

    public static void IncrementMoveCount() {
        moveCount++;
    }

    public static void undo() {
        if (prevSylvieR == -1) {
            return;
        }
        if (prevSylvieC + 1 < 7 && constellationArr[prevSylvieR, prevSylvieC + 1] != null) {
            constellationArr[prevSylvieR,prevSylvieC + 1].gameObject.GetComponent<Button>().interactable = true ;
        }
        if (prevSylvieC - 1 >= 0 && constellationArr[prevSylvieR, prevSylvieC - 1] != null) {
            constellationArr[prevSylvieR,prevSylvieC - 1].gameObject.GetComponent<Button>().interactable = true;
        }
        if (prevSylvieR + 1 < 7 && constellationArr[prevSylvieR + 1, prevSylvieC] != null) {
        constellationArr[prevSylvieR + 1,prevSylvieC].gameObject.GetComponent<Button>().interactable = true;
        }
        if (prevSylvieR - 1 >= 0 && constellationArr[prevSylvieR - 1, prevSylvieC] != null) {
           constellationArr[prevSylvieR - 1,prevSylvieC].gameObject.GetComponent<Button>().interactable = true ;
        }
        //disable old adjacent buttons
        if (curSylvieC + 1 < 7 && constellationArr[curSylvieR, curSylvieC + 1] != null) {
            constellationArr[curSylvieR, curSylvieC + 1].gameObject.GetComponent<Button>().interactable = false ;
        }
        if (curSylvieC - 1 >= 0 && constellationArr[curSylvieR, curSylvieC - 1] != null) {
            constellationArr[curSylvieR,curSylvieC - 1].gameObject.GetComponent<Button>().interactable = false ;
        }
        if (curSylvieR + 1 < 7 && constellationArr[curSylvieR  + 1, curSylvieC] != null) {
            constellationArr[curSylvieR + 1,curSylvieC].gameObject.GetComponent<Button>().interactable = false ;
        }
        if (curSylvieR - 1 >= 0 && constellationArr[curSylvieR - 1, curSylvieC] != null) {
            constellationArr[curSylvieR - 1,curSylvieC].gameObject.GetComponent<Button>().interactable = false ;
        }
        //update the saved position
        curSylvieR = prevSylvieR;
        curSylvieC = prevSylvieC;
        prevSylvieR = -1;
        prevSylvieC = -1;
        sylviePos.position = new Vector3(constellationArr[curSylvieR,curSylvieC].position.x, constellationArr[curSylvieR,curSylvieC].position.y,0);

        //update Pollus' position
        if (prevPollusR < 7 && prevPollusR >= 0 && prevPollusC < 7 && prevPollusC >= 0 && constellationArr[prevPollusR,prevPollusC] != null) {
            pollusPos.position = new Vector3(constellationArr[prevPollusR,prevPollusC].position.x, constellationArr[prevPollusR,prevPollusC].position.y,0) ;
            
            curPollusR = prevPollusR;
            curPollusC = prevPollusC;
            prevPollusR = -1;
            prevPollusC = -1;
        }

        //update Castor's position
        if (prevCastorR < 7 && prevCastorR >= 0 && prevCastorC < 7 && prevCastorC >= 0 && constellationArr[prevCastorR, prevCastorC] != null) {
            castorPos.position = new Vector3(constellationArr[prevCastorR,prevCastorC].position.x, constellationArr[prevCastorR,prevCastorC].position.y,0) ;
            
            curCastorR = prevCastorR;
            curCastorC = prevCastorC;
            prevCastorR = -1;
            prevCastorC = -1;
        }
        
    }
    void Update()
    {
        if (!hasDone) {
            if (curCastorR == targetPositionr1 && curCastorC == targetPositionc1) {
                if (curPollusR == targetPositionr2 && curPollusC == targetPositionc2) {
                    //insert ending sequence here
                    hasDone = true;
                    StartCoroutine(Completed());
                }
            }
            if (curPollusR == targetPositionr1 && curPollusC == targetPositionc1 && !hasDone) {
                if (curCastorR == targetPositionr2 && curCastorC == targetPositionc2) {
                    //insert ending sequence here
                    hasDone = true;
                    StartCoroutine(Completed());
                }
            }
        }
    }
    IEnumerator Completed()
    {
        AudioManager.Instance.FadeMusic(true, true);
        NotificationManager.Instance.TestPuzzleCompleteNotification();
        yield return new WaitForSeconds(4f);
        proc.GetComponent<PuzzleProc>().PuzzleInit();
    }
}
