using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SYLVIE_STAGE {
    BABY, CHILD, TEEN, ADULT
}
public class EXPManager : MonoBehaviour
{
    [SerializeField] private static int currentExp = 0;
    [SerializeField] private static int expUntilLevel = 100;
    [SerializeField] private static SYLVIE_STAGE stage = SYLVIE_STAGE.BABY;
    [SerializeField] public GameObject player;
    [SerializeField] public static Sprite childSprite;
    [SerializeField] public static Sprite teenSprite;
    [SerializeField] public static Sprite adultSprite;

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
}
