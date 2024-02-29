using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public static UnityAction OnLevelGenerated;

    [Header(" Settings ")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Level[] levels;
    private int level;

    private void Awake()
    {
        Time.timeScale = 1;

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        LoadData();

        UIManager.onNextLevelButtonPressed += SetNextLevel;
    }

    private void OnDestroy()
    {
        UIManager.onNextLevelButtonPressed -= SetNextLevel;
    }

    void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        int levelIndex = level % levels.Length;
        
        Level levelInstance = Instantiate(levels[levelIndex], transform);

        playerTransform.position = levelInstance.GetPlayerStartPosition();

        OnLevelGenerated?.Invoke();
    }
    
    private void IncreaseLevelIndex()
    {
        level++;
        SaveData();
    }
    
    private void SetNextLevel()
    {
        IncreaseLevelIndex();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    private void LoadData()
    {
        level = PlayerPrefs.GetInt("LEVEL");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("LEVEL", level);
    }

    public Level GetLevel()
    {
        return levels[level % levels.Length];
    }
}
