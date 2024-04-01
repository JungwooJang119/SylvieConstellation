using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton class that holds persistent data and handles transitions;
/// </summary>

public class GameManager : Singleton<GameManager> {

    [SerializeField] private int[] levelIndeces;

    /// <summary> Internal reference to the active GameManager; </summary>
    // private static GameManager _instance;
    // /// <summary> Public instance reference to the active GameManager; </summary>
    // public static GameManager Instance => _instance;

    public int ConstellationSceneTransfer = 2;

    void Awake() {
        // /// Initialize Singleton;
        // if (_instance != null) {
        //     Destroy(gameObject);
        // } else {
        //     _instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        InitializeSingleton();
    }
}
