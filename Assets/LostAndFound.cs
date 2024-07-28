using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using TMPro;

public class LostAndFound : MonoBehaviour
{

    public const int totalLostObjects = 4;
    public GameObject[] objectList = new GameObject[totalLostObjects + 1]; //index 0 is npc
    private Vector3[] startPosList = new Vector3[totalLostObjects + 1];
    private Vector3[] endPosList = new Vector3[totalLostObjects + 1];
    private bool[] isCollected = new bool[totalLostObjects + 1];


    //below used for testing
    public SpriteRenderer[] spriteList = new SpriteRenderer[totalLostObjects + 1];
    public TextMeshProUGUI textComponent;


    public bool begin = false;
    private bool collided = false;
    public int collected = 0; //max is totalLostObjects

    /*
    ****COMPLETED: set of condition to check if collide with the npc sprite color (red is too far, blue is close)
    ****COMPLETED: set condition to check if play satisfy all requirement to finish quest
                   includes picked ALL objects and close to npc (npc will turns green)
    ****COMPLETED: can partially submit each objects, multiple objects, or all objects (with orbit control)

    ****COMPLETED: hide objects before quest or give up quest, set active after accepted quest (press Space when close to npc)
    ****COMPLETED: available if player quit quest (press Y anywhere anytime) or just want to play again after finished

    ****COMPLETED: after player collected objects, when move close to npc automatically object appear next to npc
                or need a condition like pressing space to give objects to npc


    ****CANCELLED: add dialogue or text with condition of pressing space bar and click on icon
    ****CANCELLED: add arrows of direction to the closest object to be collected


    ****PROBLEM: npc also influenced by orbit control, need to edit orbit control to only works on certain objects (use tags?)
                which excludes npc, constellations, background pillar ruins, etc
    ****PROBLEM: objects collected and appear next to npc still affected by the player orbit control


    */

    // Start is called before the first frame update
    void Start()
    {
        startPosList[0] = objectList[0].transform.position;
        spriteList[0] = objectList[0].GetComponent<SpriteRenderer>();
        endPosList[0] = startPosList[0];

        for (int i = 1; i < totalLostObjects + 1; i++) {
            startPosList[i] = objectList[i].transform.position;
            spriteList[i] = objectList[i].GetComponent<SpriteRenderer>();

            float radians = i * 2 * Mathf.PI / totalLostObjects;
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);
            Vector3 location = new Vector3(horizontal, vertical, 0);
            endPosList[i] = startPosList[0] + location;

            objectList[i].SetActive(false);

            isCollected[i] = false;
        }




    }

    // Update is called once per frame
    void Update() {
        objectList[0].transform.position = startPosList[0];
        //because npc is also affected by orbit control, only needed during testing to make npc unmovable



        if (collided && Input.GetKeyDown(KeyCode.Space)) { //trigger npc
            if (!begin) {
                ResetGame(true);
            } else if (begin && collected == totalLostObjects) {
                ResetGame(false);
            }
        }

        if (begin && collided) {
            RunGame();
        }

        //these ifs are for checking conditions of the npc
        if (collided && collected == totalLostObjects) {
            spriteList[0].color = new Color(0, 1, 0, 1); //green npc if finish the quest and close to npc
        } else if (collided) {
            spriteList[0].color = new Color(0, 0, 1, 1); //blue npc if close to the npc
        } else {
            spriteList[0].color = new Color(1, 0, 0, 1); //red npc if far from the npc
        }


        for (int i = 1; i < totalLostObjects; i++) {
            objectList[i].transform.hasChanged = false; //reset if objects changed position due to orbit control
        }

        if (Input.GetKeyDown(KeyCode.Y)) { //quit game anytime anywhere, maybe a pop up message if they want to quit?
            ResetGame(false);
        }

        ChangeText(); //for testing
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.Equals(objectList[0])) {
            collided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.Equals(objectList[0])) {
            collided = false;
        }
        for (int i = 1; i < objectList.Length; i++) { //if the object is left behind (i.e black hole), return to original position
            if (other.gameObject.Equals(objectList[i]) && !isCollected[i]) {
                objectList[i].transform.position = startPosList[i];
                objectList[i].transform.hasChanged = false;
            }
        }
    }


    private void ResetGame(bool visible) {
        for (int i = 1; i < objectList.Length; i++) {
            objectList[i].transform.position = startPosList[i];
            objectList[i].transform.hasChanged = false;
            objectList[i].SetActive(visible); //objects will visible after trigger the quest or not
            isCollected[i] = false;
        }
        collected = 0;
        begin = visible;

    }

    private void RunGame() { //after accepted quest
        for (int i = 1; i < objectList.Length; i++) {
            if (objectList[i].transform.hasChanged && !isCollected[i]) {
            //detect if being used by orbit control && not collected yet, objectList[i].activeSelf
                //objectList[i].SetActive(false);
                collected++;
                objectList[i].transform.position = endPosList[i];
                isCollected[i] = true;

            } else if (isCollected[i]) {
                objectList[i].transform.position = endPosList[i]; //for the sake of orbit control still affect returned objects
            }

            //just an idea if objects and npc not affected by orbit control, after all objects collected with rotate around npc
            if (collected == totalLostObjects) {
                objectList[i].transform.RotateAround(startPosList[0], Vector3.forward, 1f);
            }

        }
    }

    private void ChangeText() {//for testing purpose
        if (textComponent == null) {
            return;
        }
        if (!begin) {
            textComponent.text = "not begin yet. move close to npc and press space to begin, press y anywhere to quit game";
        } else if (begin && collected == totalLostObjects) {
            textComponent.text = "congrat, u finished the game. press space or y to restart";
        } else if (begin && !collided) {
            textComponent.text = "game started, move CLOSER! collected " + collected + ", goal is " + totalLostObjects;
        } else if (begin && collided) {
            textComponent.text = "game started, stay close. collected " + collected + ", goal is " + totalLostObjects;
        }
    }
}
