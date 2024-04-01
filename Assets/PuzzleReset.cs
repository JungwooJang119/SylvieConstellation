using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleReset : MonoBehaviour
{
    public Button Button;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = Button.GetComponent<Button>();
        btn.onClick.AddListener(Reset);
    }
    public void Reset() {
        Debug.Log("Button Pressed!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
