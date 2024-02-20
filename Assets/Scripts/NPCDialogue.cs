using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class NPCDialogue : MonoBehaviour
{
    private State currentState;

    public DialogueRunner dialogueRunner;
    // Start is called before the first frame update
    void Start()
    {
        currentState = new IdleState(dialogueRunner);
        currentState.OnEnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //dialogueRunner.StartDialogue("LoversNPC");
            string dialogueAnswer;
            dialogueRunner.VariableStorage.TryGetValue("$LoversNPCState", out dialogueAnswer);
            Debug.Log($"LoversNPCState: {dialogueAnswer}");
            if (dialogueAnswer.Equals("Affirmative"))
            {
                ChangeDialogueState(new IncompleteTaskState(dialogueRunner));
            }
            else
            {
                ChangeDialogueState(new IdleState(dialogueRunner));
            }
            currentState.OnExecuteState(this);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void ChangeDialogueState(State newState)
    {
        currentState.OnExitState(this);
        currentState = newState;
        newState.OnEnterState(this);
        Debug.Log($"STATE: {currentState.GetType()}");
    }
}
