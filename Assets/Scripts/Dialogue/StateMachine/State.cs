using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Called when the entity's state is transitioned to enter
    /// a new state.
    /// </summary>
    public abstract void OnEnterState();

    /// <summary>
    /// Called in the entity's Update() method.  Will eventually
    /// modify to require the NPC entity to be passed in as a
    /// parameter.
    /// </summary>
    public abstract void OnExecuteState();

    /// <summary>
    /// Called when the entity's state is exiting from the
    /// old state.
    /// </summary>
    public abstract void OnExitState();
}
