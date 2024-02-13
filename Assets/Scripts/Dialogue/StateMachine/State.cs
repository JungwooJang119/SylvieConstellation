using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public abstract class State
{
    protected DialogueRunner dialogueRunner;

    public State(DialogueRunner dialogueRunner)
    {
        this.dialogueRunner = dialogueRunner;
    }

    

    /// <summary>
    /// Called when the entity's state is transitioned to enter
    /// a new state.
    /// </summary>
    public abstract void OnEnterState(NPCDialogue npcDialogue);

    /// <summary>
    /// Called in the entity's Update() method.  Will eventually
    /// modify to require the NPC entity to be passed in as a
    /// parameter.
    /// </summary>
    public abstract void OnExecuteState(NPCDialogue npcDialogue);

    /// <summary>
    /// Called when the entity's state is exiting from the
    /// old state.
    /// </summary>
    public abstract void OnExitState(NPCDialogue npcDialogue);
}
