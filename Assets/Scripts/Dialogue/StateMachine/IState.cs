using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{

    /// <summary>
    /// Called when the entity's state is transitioned to enter
    /// a new state.
    /// </summary>
    void OnEnterState();

    /// <summary>
    /// Called in the entity's Update() method.  Will eventually
    /// modify to require the NPC entity to be passed in as a
    /// parameter.
    /// </summary>
    void OnExecuteState();

    /// <summary>
    /// Called when the entity's state is exiting from the
    /// old state.
    /// </summary>
    void OnExitState();
}
