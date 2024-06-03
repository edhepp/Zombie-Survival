using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager Instance { get; private set; }

    [Header("Audio Source Prefab")]
    public GameObject audioSourcePrefab;

    [Header("Initial Pool Size")]
    [SerializeField] private int initialPoolSize = 10;

    [SerializeField] private int _maxPoolSize = 20;

    private List<AudioSource> audioSources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize the audio sources pool
            audioSources = new List<AudioSource>();
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreateNewAudioSource();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Sound clips
        //Barrier breaking
        //Game over Sound FX
        //Gun shots
        //Impact sound
        //player dies
        //Zombie dies
        //Repairing barrier
    private AudioSource CreateNewAudioSource()
    {
        GameObject audioSourceObject = Instantiate(audioSourcePrefab, transform);
        audioSourceObject.transform.parent = transform; // Set as child of the SFXManager GameObject
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
        audioSources.Add(audioSource);
        return audioSource;
    }

    public void PlaySFX(AudioClip clip)
    {
        // Find an available audio source
        var availableAudioSource = audioSources.FirstOrDefault(source => !source.isPlaying);

        if (availableAudioSource == null)
        {
            // No available audio source, create a new one, if max use default
            if (audioSources.Count >= _maxPoolSize)
            {
                availableAudioSource = audioSources[0];
                return;
            }
            availableAudioSource = CreateNewAudioSource();
        }
        availableAudioSource.PlayOneShot(clip);
    }
}