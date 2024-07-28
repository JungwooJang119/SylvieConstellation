using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This script is attached to the EXPManager prefab.
 * Ideally, the prefab is present in all scenes.
 * In puzzle scenes, the prefab will add EXP upon completion.
 * In shopping scenes, the prefab will perform EXP transactions.
 * In all scenes, the prefab will ensure that the correct sprite
 * is displayed depending on player level.
*/
//enum to track level progression
public enum SYLVIE_STAGE {
    BABY , CHILD, TEEN, ADULT
}
public class EXPManager : MonoBehaviour
{
    //total EXP that the player has collected
    [SerializeField] private static int currentExp = 0;
    //EXP that the player still needs to level up
    [SerializeField] private static int expUntilLevel = 100;
    //level threshold -> subject to change
    [SerializeField] private static int THRESHOLD = 100;
    //the current stage that needs to be displayed
    [SerializeField] private static SYLVIE_STAGE stage = SYLVIE_STAGE.BABY;
    //potential sprites, currently store placeholders
    [SerializeField] public Sprite babySprite;
    [SerializeField] public Sprite childSprite;
    [SerializeField] public Sprite teenSprite;
    [SerializeField] public Sprite adultSprite;
    //reference to the player
    [SerializeField] public GameObject player;
    //reference to the puzzle manager object
    [SerializeField] public GameObject puzzleManager;
    //the current puzzle 
    [SerializeField] private PuzzleManagement.PuzzleID puzzle;
    //the amount to add per level -> subject to change
    [SerializeField] private static int majorEXP = 25;
    [SerializeField] private static int minorEXP = 15;
    [SerializeField] private static PuzzleManagement.PuzzleID[] mainPuzzles = {PuzzleManagement.PuzzleID.Perseus, PuzzleManagement.PuzzleID.Dionysus, PuzzleManagement.PuzzleID.DragonHotDude};


    //adds to total EXP for puzzle completion
    public void addEXP(int exp) {
        //only add EXP if the amount to add is positive
        if (exp > 0) {
            currentExp += exp;
            //update expUntilLevel if the player isn't maxxed out
            if (stage != SYLVIE_STAGE.ADULT) {
                expUntilLevel -= exp;
            } else {
                return;
            }
        }
        //level up if necessary
        if (expUntilLevel <= 0) {
            updateLevel();
            //reset expUntilLevel unless maxxed out
            if (stage != SYLVIE_STAGE.ADULT) {
                expUntilLevel = THRESHOLD;
            }
        }
    }
    
    //removes total EXP for the shop
    //note that this does not take away from level progress
    public void spendEXP(int exp) {
        if (exp > 0) {
            currentExp -= exp;
        }
    }

    //transitions Sylvie to the next level when necessary
    private void updateLevel() {
        switch(stage) {
            case SYLVIE_STAGE.BABY:
                stage = SYLVIE_STAGE.CHILD;
                break;
            case SYLVIE_STAGE.CHILD:
                stage = SYLVIE_STAGE.TEEN;
                break;
            case SYLVIE_STAGE.TEEN:
                stage = SYLVIE_STAGE.ADULT;
                break;
        }
        updateSprite();
    }

    //update Sylvie's appearance based on their level
    //note that the code functions but the appearance does not change due to the animator
    private void updateSprite() {
        Debug.Log("updating appearance");
        if (player == null) {
            return;
        }
        //add something for the animator when ready
        switch(stage) {
            case SYLVIE_STAGE.BABY:
                Debug.Log("baby appearance");
                player.gameObject.GetComponent<SpriteRenderer>().sprite = babySprite;
                break;
            case SYLVIE_STAGE.CHILD:
                Debug.Log("child appearance");
                player.gameObject.GetComponent<SpriteRenderer>().sprite = childSprite;
                break;
            case SYLVIE_STAGE.TEEN:
                Debug.Log("teen appearance");
                player.gameObject.GetComponent<SpriteRenderer>().sprite = teenSprite;
                break;
            case SYLVIE_STAGE.ADULT:
                Debug.Log("adult appearance");
                player.gameObject.GetComponent<SpriteRenderer>().sprite = adultSprite;
                break;
        }
    }

    //getter methods for use with the SaveSystem
    public SYLVIE_STAGE getStage() {
        return stage;
    }
    public int getCurrentExp() {
        return currentExp;
    }
    public int getExptUntilLevel() {
        return expUntilLevel;
    }
    
    //Setter methods for retrieving from SaveSystem
    public void GetSaveFile(SYLVIE_STAGE s, int currentE, int eLeft) {
        currentExp = currentE;
        expUntilLevel = eLeft;
        stage = s;
    }
    //determines if something is a main puzzle or not -> fix
    private bool isMainPuzzle(PuzzleManagement.PuzzleID p) {
        foreach (PuzzleManagement.PuzzleID puzzles in mainPuzzles) {
            if(p.Equals(puzzles)) {
                return true;
            }
        }
        return false;
    }

    //adjusts Sylvie's appearance at the start of the load
    void Start() {
        puzzle = (PuzzleManagement.PuzzleID)SceneManager.GetActiveScene().buildIndex;
        updateSprite();
        
    }
    void Update() {
        //for testing
        if(Input.GetKeyDown(KeyCode.Tab)) {
            addEXP(majorEXP);
            Debug.Log(currentExp + ", " + expUntilLevel + ", " + stage);
        }
        //puzzle transition workaround, Perseus scene won't switch scenes :(
        // if(Input.GetKeyDown(KeyCode.K)) {
        //     SceneManager.LoadScene("Perseus");
        // }
        // if(Input.GetKeyDown(KeyCode.Tab)) {
        //     Debug.Log("L is pressed");
        //     puzzleManager.gameObject.GetComponent<PuzzleManagement.PuzzleManager>().CompletePuzzle((PuzzleManagement.PuzzleID)2);
        //     SceneManager.LoadScene("EXPTestWorld");
        //     Debug.Log(currentExp + ", " + expUntilLevel + ", " + stage);
        // }
        
        //for actual gameplay
        if (puzzle != PuzzleManagement.PuzzleID.MainWorld && puzzleManager.gameObject.GetComponent<PuzzleManagement.PuzzleManager>().GetPuzzleStatus(puzzle)) {
            if (isMainPuzzle(puzzle)) {
                addEXP(majorEXP);
            } else {
                addEXP(minorEXP);
            }
        }
    }
}
