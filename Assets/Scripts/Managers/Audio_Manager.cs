using System.Collections;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager Instance { get; private set; }

    [Header("Audio Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip inGameMusic;
    public AudioClip gameOverMusic;

    private AudioSource audioSource;
    private float userVolume = 1.0f; // Assume user volume is set to 1.0 (100%) by default

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = userVolume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameManager.OnMainMenu += PlayMainMenuMusic;
        GameManager.OnInGame += PlayInGameMusic;
        GameManager.OnGameOver += PlayGameOverMusic;
    }

    private void OnDisable()
    {
        GameManager.OnMainMenu -= PlayMainMenuMusic;
        GameManager.OnInGame -= PlayInGameMusic;
        GameManager.OnGameOver -= PlayGameOverMusic;
    }

    private void PlayMainMenuMusic()
    {
        StartCoroutine(SwitchTrack(mainMenuMusic));
    }

    private void PlayInGameMusic()
    {
        StartCoroutine(SwitchTrack(inGameMusic));
    }

    private void PlayGameOverMusic()
    {
        StartCoroutine(SwitchTrack(gameOverMusic));
    }

    private IEnumerator SwitchTrack(AudioClip newClip)
    {
        if (audioSource.clip == newClip) yield break;

        if (audioSource.isPlaying)
        {
            // Fade out the current track
            yield return StartCoroutine(FadeOut());
        }

        // Change the track and fade in the new track
        audioSource.clip = newClip;
        audioSource.Play();
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut(float fadeDuration = 1.0f)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    private IEnumerator FadeIn(float fadeDuration = 1.0f)
    {
        audioSource.volume = 0;
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, userVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = userVolume;
    }

    public void SetUserVolume(float volume)
    {
        userVolume = volume;
        audioSource.volume = userVolume;
    }
}