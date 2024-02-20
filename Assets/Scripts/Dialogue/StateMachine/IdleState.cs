using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class IdleState : State
{

    public IdleState(DialogueRunner dialogueRunner) : base(dialogueRunner)
    {

    }

    public override void OnEnterState(NPCDialogue npcDialogue)
    {

    }

    public override void OnExecuteState(NPCDialogue npcDialogue)
    {
        dialogueRunner.StartDialogue("LoversNPC");
        npcDialogue.ChangeDialogueState(new FirstMeetingState(dialogueRunner));
    }

    public override void OnExitState(NPCDialogue npcDialogue)
    {

    }
}
