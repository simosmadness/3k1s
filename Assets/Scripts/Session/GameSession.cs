using Game.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    public PlayerStatsModel PlayerStats { get; } = new PlayerStatsModel();
    public LevelModel Level { get; } = new LevelModel();
    public SettingsModel Settings { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Settings = new SettingsModel();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        if (s.buildIndex == 0) return; // MainMenu
#if UNITY_2023_1_OR_NEWER
        int enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
#else
        int enemies = FindObjectsOfType<Enemy>().Length;
#endif
        Level.Begin(enemies);
    }

    void Update()
    {
        // Time.deltaTime == 0 при Time.timeScale == 0 → пауза автоматически стопорит таймер.
        Level.Tick(Time.deltaTime);
    }

    // Обработчики переходов между уровнями — модельные события.
    void Start()
    {
        Level.LevelWon += LoadNextLevel;
    }

    void OnDestroySession() { Level.LevelWon -= LoadNextLevel; }

    void LoadNextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(next < SceneManager.sceneCountInBuildSettings ? next : 0);
    }
}