using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton class. Handles screen space fade and scene transitions;
/// </summary>
public class TransitionManager : MonoBehaviour {

    public static TransitionManager Instance { get; private set; }

    [SerializeField]  CanvasGroup canvasGroup;
    private Coroutine transition;
    public Vector2 holdPos;
    private int counter = -1;
    public Vector2 targetPosition;
    
    public Vector2 consPos1;
    public Vector2 consPos2;
    public Vector2 consPos3;
    public Vector2 consPos4;
    public Vector2 consPos5;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
        targetPosition = consPos1; 
    }

    /// <summary>
    /// Load the scene with the given build index;
    /// </summary>
    /// <param name="sceneIndex"> Index of the scene to load; </param>
    public void GoToScene(int sceneIndex) => StartCoroutine(_GoToScene(sceneIndex));

    /// <summary>
    /// Set the opacity of the transition canvas to the given value;
    /// </summary>
    /// <returns> A coroutine that may be used to track when the fade ends; </returns>
    public Coroutine Fade(float targetAlpha) => StartCoroutine(_Fade(targetAlpha));

    /// <summary>
    /// Scene load coroutine; <br></br>
    /// NOTE: Consider switching to asynchronous scene loading for smoother transitions;
    /// </summary>
    private IEnumerator _GoToScene(int sceneIndex) {
        transition = Fade(1);
        if (sceneIndex == 3) {
            counter++;
            if (counter == 1) {
                targetPosition = consPos2;
                holdPos = consPos1;
            } else if (counter == 2) {
                targetPosition = consPos3;
                holdPos = consPos2;
            } else if (counter == 3) {
                targetPosition = consPos4;
                holdPos = consPos3;
            } else if (counter == 4) {
                targetPosition = consPos5;
                holdPos = consPos4;
            }
        }
        while (transition != null) {
            yield return null;
        }
        SceneManager.LoadScene(sceneIndex);
        Fade(0);
    }

    /// <summary> Transition canvas fade coroutine; </summary>
    private IEnumerator _Fade(float targetAlpha) {
        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha)) {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Mathf.Min(0.1f, Time.unscaledDeltaTime));
            yield return null;
        }
        bool isOpaque = !Mathf.Approximately(canvasGroup.alpha, 0);
        canvasGroup.blocksRaycasts = isOpaque;
        canvasGroup.interactable = isOpaque;
        transition = null;
    }
}