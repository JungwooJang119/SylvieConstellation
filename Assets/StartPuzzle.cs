using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleManagement;
using UnityEngine.SceneManagement;

public class StartPuzzle : MonoBehaviour
{

    private bool canGo;
    public int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canGo && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(sceneIndex);
        }
    }
    
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            canGo = true;
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            canGo = false;
        }
    }
}
