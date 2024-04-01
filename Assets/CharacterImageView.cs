using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class CharacterImageView : MonoBehaviour
{
    private readonly Dictionary<string, string> nameToSpritePath = new Dictionary<string, string>
    {
        { "Lyra", "Assets/Art/Female_Lover.png" },
        { "Sylvie", "Assets/Art/Pillar ruins.png" }
    };

    [SerializeField]
    public Image characterDialogueImage;

    [SerializeField]
    private TextMeshProUGUI characterNameText;

    void Start()
    {
        
    }

    // void Update()
    // {
    //     if (characterNameText != null && nameToSpritePath.ContainsKey(characterNameText.text))
    //     {

    //         string spritePath = nameToSpritePath[characterNameText.text];
    //         Debug.Log($"SPRITE PATH: {spritePath}");
    //         characterDialogueImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
    //         Debug.Log($"SPRITE: {characterDialogueImage.sprite}");

    //     }
    // }
}
