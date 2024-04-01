using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class FirstMeetingState : State
{
    
    public FirstMeetingState(DialogueRunner dialogueRunner) : base(dialogueRunner)
    {

    }

    public override void OnEnterState(NPCDialogue npcDialogue)
    {
        Debug.Log("Entered First Meeting State");
    }

    public override void OnExecuteState(NPCDialogue npcDialogue)
    {
        string dialogueAnswer;
        dialogueRunner.VariableStorage.TryGetValue($"${npcDialogue.statusVar}", out dialogueAnswer);
        if (dialogueAnswer.Equals("Affirmative"))
        {
            npcDialogue.ChangeDialogueState(new IncompleteTaskState(dialogueRunner, npcDialogue.taskInProgressStateDialogueTitle));
        }
        else
        {
            npcDialogue.ChangeDialogueState(new IdleState(dialogueRunner, npcDialogue.idleStateDialogueTitle));
        }
    }


    public override void OnExitState(NPCDialogue npcDialogue)
    {
        dialogueRunner.Stop();
    }
}
