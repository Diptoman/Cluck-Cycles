using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    #region Singleton script
    public static MusicController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log($"Init instance of Slot controller");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log($"Destroy instance of Slot controller");
            Destroy(gameObject);
        }
    }

    public static void Destroy()
    {
        Instance = null;
    }
    #endregion

    [SerializeField]
    private AudioClip[] musicTracks;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource[] SFXsources;

    private int currentTrackIndex = 0;
    public bool IsSFXOn = true, IsMusicOn = true;

    void Start()
    {
        if (musicTracks.Length > 0)
        {
            PlayNextTrack();
        }
    }

    void Update()
    {
        // Check if current track finished playing
        if (!audioSource.isPlaying && musicTracks.Length > 0)
        {
            PlayNextTrack();
        }
    }

    public void TurnOffMusic()
    {
        audioSource.Stop();
    }

    void PlayNextTrack()
    {
        if (!IsMusicOn)
            return;

        currentTrackIndex = Random.Range(0, musicTracks.Length);

        audioSource.clip = musicTracks[currentTrackIndex];
        audioSource.Play();
    }

    public void PlaySFX(AudioClip clip, bool force = false)
    {
        if (!IsSFXOn)
            return;

        if (force == true)
        {
            SFXsources[0].clip = clip;
            SFXsources[0].Play();
        }
        else
        {
            foreach (AudioSource sfxSource in SFXsources)
            {
                if (!sfxSource.isPlaying)
                {
                    sfxSource.clip = clip;
                    sfxSource.Play();
                    break;
                }
            }
        }
    }
}
