using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class IdleState : State
{

    private string dialogScriptTitle;
    public IdleState(DialogueRunner dialogueRunner, string dialogScriptTitle) : base(dialogueRunner)
    {
        this.dialogScriptTitle = dialogScriptTitle;
    }

    public override void OnEnterState(NPCDialogue npcDialogue)
    {

    }

    public override void OnExecuteState(NPCDialogue npcDialogue)
    {
        dialogueRunner.StartDialogue(dialogScriptTitle);
        npcDialogue.ChangeDialogueState(new FirstMeetingState(dialogueRunner));
    }

    public override void OnExitState(NPCDialogue npcDialogue)
    {

    }
}
