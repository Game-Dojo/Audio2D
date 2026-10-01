using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string lives = "PlayerLives ";
    private void Start()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(lives, 3);
    }

    public void Load()
    {
        var vidas = PlayerPrefs.GetInt("PlayerLives", 99);
        print("Total de vidas: " + vidas.ToString());
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("PlayerLives", 3);
        
        PlayerPrefs.SetFloat("PlayerX", 30);
        PlayerPrefs.SetFloat("PlayerY", 20);
        PlayerPrefs.Save();
    }
}
