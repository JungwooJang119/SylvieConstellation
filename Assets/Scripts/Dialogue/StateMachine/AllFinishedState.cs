using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AllFinishedState : State
{
    private string dialogScriptTitle;
    public AllFinishedState(DialogueRunner dialogueRunner, string dialogScriptTitle) : base(dialogueRunner)
    {
        this.dialogScriptTitle = dialogScriptTitle;
    }

    public override void OnEnterState(NPCDialogue npcDialogue)
    {
        Debug.Log("Entered Final State");

    }

    public override void OnExecuteState(NPCDialogue npcDialogue)
    {

        dialogueRunner.StartDialogue(dialogScriptTitle);

    }

    public override void OnExitState(NPCDialogue npcDialogue)
    {

    }
}
