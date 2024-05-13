using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SYLVIE_STAGE {
    BABY, CHILD, TEEN, ADULT
}
public class EXPManager : MonoBehaviour
{
    //total EXP that the player has collected
    [SerializeField] private static int currentExp = 0;
    //EXP that the player still needs to level up
    [SerializeField] private static int expUntilLevel = 100;
    //the current stage that needs to be displayed
    [SerializeField] private static SYLVIE_STAGE stage = SYLVIE_STAGE.BABY;
    //potential sprites
    [SerializeField] public Sprite childSprite;
    [SerializeField] public Sprite teenSprite;
    [SerializeField] public Sprite adultSprite;
    //reference to the player
    [SerializeField] public GameObject player;
    //reference to the puzzle manager object
    [SerializeField] public GameObject puzzleManager;
    //the current puzzle, is 0 in open world
    [SerializeField] private PuzzleManagement.PuzzleID  puzzle = 0;
    //the amount to add per level
    [SerializeField] private int majorEXP = 25;
    [SerializeField] private int minorEXP = 15;
    public static void addEXP(int exp) {
        if (exp > 0) {
            currentExp += exp;
            expUntilLevel -= exp;
        }
        if (expUntilLevel <= 0) {
            updateLevel();
            expUntilLevel = 100;
        }
    }
    public static void spendEXP(int exp) {
        if (exp > 0) {
            currentExp -= exp;
        }
    }

    private static void updateLevel() {
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
    }

    private void updateSprite() {
        switch(stage) {
            case SYLVIE_STAGE.CHILD:
                player.gameObject.GetComponent<SpriteRenderer>().sprite = childSprite;
                break;
            case SYLVIE_STAGE.TEEN:
                player.gameObject.GetComponent<SpriteRenderer>().sprite = teenSprite;
                break;
            case SYLVIE_STAGE.ADULT:
                player.gameObject.GetComponent<SpriteRenderer>().sprite = adultSprite;
                break;
        }
    }
    //fix
    private bool isMainPuzzle(PuzzleManagement.PuzzleID p) {
        return true;
    }

    void start() {
        updateSprite();
    }
    void update() {
        Debug.Log("hello");
        //for testing
        if(Input.GetKeyDown(KeyCode.Tab)) {
            addEXP(majorEXP);
            Debug.Log(currentExp + ", ");
        }
        // if (puzzleManager.gameObject.GetComponent<PuzzleManagement.PuzzleManager>().GetPuzzleStatus(puzzle)) {
        //     if (isMainPuzzle(puzzle)) {
        //         addEXP(majorEXP);
        //     } else {
        //         addEXP(minorEXP);
        //     }
        // }
    }
}
