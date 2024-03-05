using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("backspace"))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        AudioManager.Instance.FadeMusic(false, false);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Quit()
    {
        #if UNITY_STANDALONE
				Application.Quit();
		#endif
		#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
		#endif
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("TransitionStart");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
