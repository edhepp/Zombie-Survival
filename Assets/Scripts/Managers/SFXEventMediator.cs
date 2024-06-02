using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SFXEventMediator : MonoBehaviour
{
    public static SFXEventMediator Instance { get; private set; }

    [Header("Barrier Destoryed Sounds")]
    public List<AudioClip> barrierSounds;
    [Header("Repairing Barrier Sounds")]
    public List<AudioClip> repairingBarrierSounds;
    [Header("Gun Shot Sounds")]
    public List<AudioClip> gunShotSounds;
    [Header("Impact Sounds")]
    public List<AudioClip> impactSounds;
    [Header("Player Sounds")]
    public List<AudioClip> playerSounds;
    [Header("Zombie Sounds")]
    public List<AudioClip> zombieSounds;
    [Header("Game Over Sounds")]
    public List<AudioClip> gameOverSounds;

    private HashSet<string> eventsThisFrame = new HashSet<string>();

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

    private void LateUpdate()
    {
        // Clear the events tracked this frame at the end of the frame
        eventsThisFrame.Clear();
    }

    public void PlaySFX(string eventName, AudioClip clip)
    {
        // Check if the event has already been triggered this frame
        if (eventsThisFrame.Contains(eventName))
        {
            return; // Skip if the event was already handled this frame
        }

        // Play the sound effect
        SFX_Manager.Instance.PlaySFX(clip);

        // Track the event to prevent multiple triggers in the same frame
        eventsThisFrame.Add(eventName);
    }

    // Example methods to play specific sounds (extend as needed)
    public void PlayBarrierBreakingSFX()
    {
        PlaySFX("BarrierBreaking", barrierSounds[Random.Range(0, barrierSounds.Count)]);
    }

    public void PlayGameOverSFX()
    {
        PlaySFX("GameOver", gameOverSounds[Random.Range(0, gameOverSounds.Count)]);
    }

    public void PlayGunShotSFX()
    {
        PlaySFX("GunShot", gunShotSounds[Random.Range(0, gunShotSounds.Count)]);
    }

    public void PlayImpactSFX()
    {
        PlaySFX("Impact", impactSounds[Random.Range(0, impactSounds.Count)]);
    }

    public void PlayPlayerDiesSFX()
    {
        PlaySFX("PlayerDies", playerSounds[Random.Range(0, playerSounds.Count)]);
    }

    public void PlayZombieDiesSFX()
    {
        PlaySFX("ZombieDies", zombieSounds[Random.Range(0, zombieSounds.Count)]);
    }

    public void PlayRepairingBarrierSFX()
    {
        PlaySFX("RepairingBarrier", repairingBarrierSounds[Random.Range(0, repairingBarrierSounds.Count)]);
    }
}
