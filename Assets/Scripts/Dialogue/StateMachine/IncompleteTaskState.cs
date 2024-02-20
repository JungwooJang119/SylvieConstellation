using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class IncompleteTaskState : State
{

    public IncompleteTaskState(DialogueRunner dialogueRunner) : base(dialogueRunner)
    {

    }

    public override void OnEnterState(NPCDialogue npcDialogue)
    {

    }

    public override void OnExecuteState(NPCDialogue npcDialogue)
    {
        dialogueRunner.StartDialogue("DefaultSecondState");
        npcDialogue.ChangeDialogueState(new IdleState(dialogueRunner));
    }

    public override void OnExitState(NPCDialogue npcDialogue)
    {

    }
}
