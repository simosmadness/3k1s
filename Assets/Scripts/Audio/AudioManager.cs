using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (GameSession.Instance == null) return;
        var s = GameSession.Instance.Settings;
        s.MusicChanged += OnMusicChanged;
        if (musicSource)
        {
            musicSource.loop = true;
            musicSource.volume = s.MusicVolume;
            if (!musicSource.isPlaying) musicSource.Play();
        }
    }

    void OnDestroy()
    {
        if (GameSession.Instance != null)
            GameSession.Instance.Settings.MusicChanged -= OnMusicChanged;
    }

    void OnMusicChanged(float v) { if (musicSource) musicSource.volume = v; }

    public void PlaySFX(AudioClip clip, Vector3 pos)
    {
        if (clip == null || GameSession.Instance == null) return;
        AudioSource.PlayClipAtPoint(clip, pos, GameSession.Instance.Settings.SfxVolume);
    }
}