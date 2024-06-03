using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text killCountText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text playAgainText;

    private int killCount = 0;
    private bool isGameOver = false;

    private void Awake()
    {
        gameOverText.gameObject.SetActive(false);
        playAgainText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        killCountText.text = $"Zombie Kills: {killCount}";
        ZombieStateBehaviour.KilledEvent += UpdateKillCount;
        GameManager.OnGameOver += GameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && isGameOver)
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.N) && isGameOver)
        {
            Application.Quit();
        }
    }

    private void UpdateKillCount()
    {
        killCount++;
        killCountText.text = $"Zombie Kills: {killCount}";
    }

    private void OnDestroy()
    {
        ZombieStateBehaviour.KilledEvent -= UpdateKillCount;
        GameManager.OnGameOver -= GameOver;
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        playAgainText.gameObject.SetActive(true);
        isGameOver = true;
    }
}
