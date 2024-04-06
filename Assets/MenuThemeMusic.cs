using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuThemeMusic : Singleton<MenuThemeMusic>
{
    void Awake()
    {
        InitializeSingleton(gameObject);
    }

    void Start() {
        AudioManager.Instance.CheckMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
