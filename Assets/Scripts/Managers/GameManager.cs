using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event System.Action OnMainMenu;
    public static event System.Action OnInGame;
    public static event System.Action OnGameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //Listen for MainMenu()
        //Listen for Ingame()
        //Listen for GameOver()
        ZombieEventMediator.ZombiesWin += GameOver;
    }

    public void MainMenu()
    {
        OnMainMenu?.Invoke();
    }

    public void InGame()
    {
        OnInGame?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
