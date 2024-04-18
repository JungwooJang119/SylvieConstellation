using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


public enum PlayerConstellationState {
    PERSEUS,
    LOVERS,
    TRICKSTER,
    DIONYSUS,
    DRACO,
    CASSIOPEIA,
    GUN,
    MINOR1,
    MINOR2
}

public class NPCDialogue : MonoBehaviour
{
    private State currentState;
    public State CurrentState
    {
        get
        {
            return currentState;
        }
    }

    private bool canTalk;

    public DialogueRunner dialogueRunner;

    public GameObject gameUI;

    // Name of status variable to get from Dialog scripts
    [Header("Dialogue Script Status Variable")]
    [SerializeField] public string statusVar;

    [Header("Dialogue Script names")]
    // File names of the yarn spinner scripts (ex. LoversNPC)
    [SerializeField] public string idleStateDialogueTitle;
    [SerializeField] public string taskInProgressStateDialogueTitle;
    [SerializeField] public string taskCompleteDialogueTitle;
    [SerializeField] public string postCompletionDialogueTitle;

    [SerializeField] public CharacterImageView characterImageView;
    [SerializeField] public Sprite charImage;
    private Sprite blankImage;

    // Start is called before the first frame update
    void Start()
    {
        blankImage = Resources.Load<Sprite>("blank");
        currentState = new IdleState(dialogueRunner, idleStateDialogueTitle);
        currentState.OnEnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.isInDialogueState)
        {
            //dialogueRunner.StartDialogue("LoversNPC");
            string dialogueAnswer;
            gameUI.SetActive(false);
            PlayerController.Instance.canMove = false;
            GameManager.Instance.isInDialogueState = true;
            dialogueRunner.VariableStorage.TryGetValue($"${statusVar}", out dialogueAnswer);
            dialogueRunner.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = charImage;

            if (dialogueAnswer.Equals("Affirmative"))
            {
                ChangeDialogueState(new IncompleteTaskState(dialogueRunner, taskInProgressStateDialogueTitle));
            }
            else if (dialogueAnswer.Equals("TalkToNPCAgain"))
            {
                ChangeDialogueState(new CompletedTaskState(dialogueRunner, taskCompleteDialogueTitle));
            }
            else if (dialogueAnswer.Equals("FinalState"))
            {
                ChangeDialogueState(new AllFinishedState(dialogueRunner, postCompletionDialogueTitle));
            }
            else if (dialogueAnswer.Equals("Beginning"))
            {
                ChangeDialogueState(new IdleState(dialogueRunner, idleStateDialogueTitle));
            }
            currentState.OnExecuteState(this);
        }
        if (!PlayerController.Instance.canMove) {
            if (!dialogueRunner.IsDialogueRunning) {
                PlayerController.Instance.canMove = true;
                GameManager.Instance.isInDialogueState = false;
                dialogueRunner.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = blankImage;
                gameUI.SetActive(true);
            }
        }
    }

    public void ChangeDialogueState(State newState)
    {
        currentState.OnExitState(this);
        currentState = newState;
        newState.OnEnterState(this);
        Debug.Log($"STATE: {currentState.GetType()}");
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            canTalk = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            canTalk = false;   
        }
    }

    [YarnCommand("show_image")]
    public void ShowImage(string filepath)
    {
        if (!filepath.Equals("NO SPRITE"))
        {

            //characterImageView.characterDialogueImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(filepath);
            Debug.Log($"SPRITE: {characterImageView.characterDialogueImage.sprite}");

        }
        else
        {
            characterImageView.characterDialogueImage.sprite = null;
            Debug.Log($"SPRITE: {characterImageView.characterDialogueImage.sprite}");
        }
    }
}
