using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CompletedTaskState : State
{

    public string dialogScriptTitle;
    public CompletedTaskState(DialogueRunner dialogueRunner, string dialogScriptTitle) : base(dialogueRunner)
    {
        this.dialogScriptTitle = dialogScriptTitle;
    }

    public override void OnEnterState(NPCDialogue npcDialogue)
    {
        dialogueRunner.VariableStorage.SetValue($"${npcDialogue.statusVar}", "TalkToNPCAgain");
        Debug.Log("Entered Second Meeting/Task Complete State");

    }

    public override void OnExecuteState(NPCDialogue npcDialogue)
    {
        dialogueRunner.StartDialogue(dialogScriptTitle);
        //npcDialogue.ChangeDialogueState(new AllFinishedState(dialogueRunner));

    }

    public override void OnExitState(NPCDialogue npcDialogue)
    {

    }
}
