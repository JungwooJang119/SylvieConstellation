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
        dialogueRunner.VariableStorage.TryGetValue("$LoversNPCState", out dialogueAnswer);
        Debug.Log($"LoversNPCState: {dialogueAnswer}");
        if (dialogueAnswer.Equals("Affirmative"))
        {
            npcDialogue.ChangeDialogueState(new IncompleteTaskState(dialogueRunner));
        }
        else
        {
            npcDialogue.ChangeDialogueState(new IdleState(dialogueRunner));
        }
    }


    public override void OnExitState(NPCDialogue npcDialogue)
    {
        dialogueRunner.Stop();
    }
}
