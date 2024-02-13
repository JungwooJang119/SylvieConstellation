using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class NPCDialogue : MonoBehaviour
{
    private State currentState;

    public DialogueRunner dialogueRunner;
    public List<State> states;
    // Start is called before the first frame update
    void Start()
    {
        // AG: Hard-coding the list seems to be the best solution so far.
        states.Add(new IdleState(dialogueRunner));
        states.Add(new FirstMeetingState(dialogueRunner));
        states.Add(new IncompleteTaskState(dialogueRunner));
        states.Add(new CompletedTaskState(dialogueRunner));
        states.Add(new AllFinishedState(dialogueRunner));

        currentState = states[0];
        currentState.OnEnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //dialogueRunner.StartDialogue("LoversNPC");
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
        currentState.OnEnterState(this);
    }
}
