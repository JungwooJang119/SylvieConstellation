using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class IncompleteTaskState : State
{

    private string dialogScriptTitle;

    public IncompleteTaskState(DialogueRunner dialogueRunner, string dialogScriptTitle) : base(dialogueRunner)
    {
        this.dialogScriptTitle = dialogScriptTitle;
    }

    public override void OnEnterState(NPCDialogue npcDialogue)
    {
        Debug.Log("Entered Start Incomplete Task State");

    }

    public override void OnExecuteState(NPCDialogue npcDialogue)
    {
        dialogueRunner.StartDialogue(dialogScriptTitle);
        //npcDialogue.ChangeDialogueState(new IdleState(dialogueRunner));
    }

    public override void OnExitState(NPCDialogue npcDialogue)
    {
        dialogueRunner.Stop();
    }
}
