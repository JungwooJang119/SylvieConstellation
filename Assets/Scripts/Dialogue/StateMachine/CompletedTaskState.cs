using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CompletedTaskState : State
{

    public CompletedTaskState(DialogueRunner dialogueRunner) : base(dialogueRunner)
    {

    }

    public override void OnEnterState(NPCDialogue npcDialogue)
    {
        dialogueRunner.VariableStorage.SetValue("$LoversNPCState", "TalkToNPCAgain");
        Debug.Log("Entered Second Meeting/Task Complete State");

    }

    public override void OnExecuteState(NPCDialogue npcDialogue)
    {
        dialogueRunner.StartDialogue("DefaultTaskCompletedState");
        //npcDialogue.ChangeDialogueState(new AllFinishedState(dialogueRunner));

    }

    public override void OnExitState(NPCDialogue npcDialogue)
    {

    }
}
