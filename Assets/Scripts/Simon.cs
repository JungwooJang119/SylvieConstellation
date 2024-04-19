using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simon : MonoBehaviour
{
    private List<int> predeterminedTaskList = new List<int>(); // Predetermined sequence
    private List<int> playerTaskList = new List<int>(); // Actual sequence to be followed
    private List<int> playerSequenceList = new List<int>();

    public List<AudioClip> buttonSoundsList = new List<AudioClip>();
    public List<Button> clickableButtons;

    public AudioClip loseSound;
    public AudioClip winSound; // Sound to play when the player wins
    public AudioSource audioSource;

    public CanvasGroup buttons;
    public GameObject startButton;

    public int maxSequenceLength = 20; // Maximum number of sequences

    // Function to slightly darken a color
    private Color DarkenColor(Color color, float amount)
    {
        return new Color(color.r * amount, color.g * amount, color.b * amount, color.a);
    }

    void Start()
    {
        buttons.gameObject.SetActive(false);
        InitializePredeterminedSequence();
    }

    void InitializePredeterminedSequence()
    {
        // Example sequence, replace with your own logic if needed
        predeterminedTaskList = new List<int> { 0, 1, 2, 1, 5, 7, 1, 1, 3, 6, 6, 7, 4, 0, 5, 1, 4, 3, 6, 2, 2, 5 };
        // You can also generate this randomly or through other means
    }

    public void AddToPlayerSequenceList(int buttonId)
    {
        playerSequenceList.Add(buttonId);
        CheckSequence();
    }

    void CheckSequence()
    {
        for (int i = 0; i < playerSequenceList.Count; i++)
        {
            if (playerTaskList[i] != playerSequenceList[i])
            {
                StartCoroutine(PlayerLost());
                return;
            }
        }

        if (playerSequenceList.Count == playerTaskList.Count)
        {
            if (playerTaskList.Count >= maxSequenceLength || playerTaskList.Count >= predeterminedTaskList.Count)
            {
                StartCoroutine(PlayerWins()); // Player wins the game
            }
            else
            {
                StartCoroutine(StartNextRound());
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartNextRound());
        startButton.SetActive(false);
        buttons.gameObject.SetActive(true);
    }

    public IEnumerator PlayerLost()
    {
        audioSource.PlayOneShot(loseSound);
        ResetGame();
        yield return new WaitForSeconds(4f);
        startButton.SetActive(true);
    }

    public IEnumerator PlayerWins()
    {
        audioSource.PlayOneShot(winSound);
        // Show victory message or perform any action to indicate the player has won
        yield return new WaitForSeconds(4f);
        // Optionally reset the game or provide options to restart
        ResetGame();
        startButton.SetActive(true); // Show start button to allow game restart
    }

    private void ResetGame()
    {
        playerSequenceList.Clear();
        playerTaskList.Clear();
        buttons.interactable = false;
        startButton.SetActive(true);
    }

    public IEnumerator HighlightButton(int buttonId)
    {
        var button = clickableButtons[buttonId];
        var originalColor = button.GetComponent<Image>().color;
        var highlightedColor = DarkenColor(originalColor, 0.5f); // Darken the button color

        button.GetComponent<Image>().color = highlightedColor;
        audioSource.PlayOneShot(buttonSoundsList[buttonId]);
        yield return new WaitForSeconds(1f);

        button.GetComponent<Image>().color = originalColor; // Revert to the original color
    }

    public IEnumerator StartNextRound()
    {
        playerSequenceList.Clear();
        buttons.interactable = false;
        yield return new WaitForSeconds(0.5f);

        if (playerTaskList.Count < predeterminedTaskList.Count)
        {
            playerTaskList.Add(predeterminedTaskList[playerTaskList.Count]); // Follow the predetermined sequence
        }

        foreach (int index in playerTaskList)
        {
            yield return StartCoroutine(HighlightButton(index));
        }
        buttons.interactable = true;
        yield return null;
    }
}
