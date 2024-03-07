using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Get key input which is Esc for this
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(paused) {
                Resume();
            } else {
                Pause();
            }
        }

    }

    //Create resume and pause functions
    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void MainMenu() {
        Time.timeScale = 1f;
        paused = false;
        TransitionManager.Instance.GoToScene(0);
    }

    public void Options() {
        Debug.Log("Options");
    }

    public void Map() {
        Debug.Log("Map");
    }
}
