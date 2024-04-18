using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using PuzzleManagement;

public class bruh : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    public NPCDialogue npcDialogue;
    public GameObject[] procs;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();


        dialogueRunner.AddCommandHandler<int>("changeThisScene", ChangeThisScene);

        
    }

    private void ChangeThisScene(int index) {
        npcDialogue.dialogueRunner.VariableStorage.SetValue($"${npcDialogue.statusVar}", "TalkToNPCAgain");
        procs[index - 2].GetComponent<PuzzleProc>().PuzzleInit();
    }
}
