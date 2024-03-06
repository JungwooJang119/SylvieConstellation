using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Simon : MonoBehaviour
{
    private List<int> playerTaskList = new List<int>();
    private List<int> playerSequenceList = new List<int>();

    public List<AudioClip> buttonSoundsList = new List<AudioClip>();

    public List<List<Color32>> buttonColors = new List<List<Color32>>();

    public List<Button> clickableButtons;

    public AudioClip loseSound;

    public AudioSource audioSource;

    public CanvasGroup buttons;

    public GameObject startButton;

    public void Awake()
    {
        buttonColors.Add(new List<Color32> { new Color32(255, 100, 100, 255), new Color32(255, 0, 0, 255) }); // Add red
        buttonColors.Add(new List<Color32> { new Color32(100, 255, 100, 255), new Color32(0, 255, 0, 255) }); // Add green
        buttonColors.Add(new List<Color32> { new Color32(100, 100, 255, 255), new Color32(0, 0, 255, 255) }); // Add blue
        buttonColors.Add(new List<Color32> { new Color32(255, 255, 100, 255), new Color32(255, 255, 0, 255) }); // Add yellow
        buttonColors.Add(new List<Color32> { new Color32(255, 100, 255, 255), new Color32(255, 0, 255, 255) }); // Add magenta
        buttonColors.Add(new List<Color32> { new Color32(100, 255, 255, 255), new Color32(0, 255, 255, 255) }); // Add cyan
        buttonColors.Add(new List<Color32> { new Color32(255, 165, 0, 255), new Color32(255, 140, 0, 255) }); // Add orange
        buttonColors.Add(new List<Color32> { new Color32(160, 32, 240, 255), new Color32(148, 0, 211, 255) }); // Add purple

        for (int i = 0; i < 8; i++)
        {
            clickableButtons[i].GetComponent<Image>().color = buttonColors[i][0];
        }
    }
    public void AddToPlayerSequenceList(int buttonId)
    {
        playerSequenceList.Add(buttonId);
        for (int i = 0; i < playerSequenceList.Count; i++)
        {
            if (playerTaskList[i] == playerSequenceList[i])
            {
                continue;
            }
            else
            {
                StartCoroutine(PlayerLost());
                return;
            }
        }
        if (playerSequenceList.Count == playerTaskList.Count)
        {
            StartCoroutine(StartNextRound());
        }
    }
    public void StartGame()
    {
        StartCoroutine(StartNextRound());
        startButton.SetActive(false);
    }

    public IEnumerator PlayerLost()
    {
        audioSource.PlayOneShot(loseSound);
        playerSequenceList.Clear();
        playerTaskList.Clear();
        yield return new WaitForSeconds(2);
        startButton.SetActive(true);
    }
    public IEnumerator HighlightButton(int buttonId)
    {
        clickableButtons[buttonId].GetComponent<Image>().color = buttonColors[buttonId][1];
        audioSource.PlayOneShot(buttonSoundsList[buttonId]);
        yield return new WaitForSeconds(1);
        clickableButtons[buttonId].GetComponent<Image>().color = buttonColors[buttonId][0];
    }
    public IEnumerator StartNextRound()
    {
        playerSequenceList.Clear();
        buttons.interactable = false;
        yield return new WaitForSeconds(1);
        playerTaskList.Add(Random.Range(0, 8));
        foreach (int index in playerTaskList)
        {
            yield return StartCoroutine(HighlightButton(index));
        }
        buttons.interactable = true;
        yield return null;
    }
}
