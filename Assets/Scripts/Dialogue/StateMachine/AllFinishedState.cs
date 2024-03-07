using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AllFinishedState : State
{

    public AllFinishedState(DialogueRunner dialogueRunner) : base(dialogueRunner)
    {

    }

    public override void OnEnterState(NPCDialogue npcDialogue)
    {
        Debug.Log("Entered Final State");

    }

    public override void OnExecuteState(NPCDialogue npcDialogue)
    {

        dialogueRunner.StartDialogue("DefaultFinalState");

    }

    public override void OnExitState(NPCDialogue npcDialogue)
    {

    }
}
