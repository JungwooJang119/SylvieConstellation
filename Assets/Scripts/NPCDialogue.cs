using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private GameObject text;
    public DialogueRunner dialogueRunner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            dialogueRunner.StartDialogue("LoversNPC");
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            SceneManager.LoadScene(1);
        }
    }
}
