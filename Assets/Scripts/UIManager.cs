using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text killCountText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text playAgainText;

    private int killCount = 0;

    private ZombieStateBehaviour zombieStateBehavior;

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
    }

    private void UpdateKillCount()
    {
        killCount++;
        killCountText.text = $"Zombie Kills: {killCount}";
    }

    private void OnDestroy()
    {
        ZombieStateBehaviour.KilledEvent -= UpdateKillCount;
    }
}
