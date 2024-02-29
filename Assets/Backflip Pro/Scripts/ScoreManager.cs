using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Text scoreText;
    private int targetScore;
    private int score;

    private void Awake()
    {
        PlayerFlipManager.OnPlayerFlipped += OnPlayerFlippedCallback;
    }

    private void OnDestroy()
    {
        PlayerFlipManager.OnPlayerFlipped -= OnPlayerFlippedCallback;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPlayerFlippedCallback()
    {
        targetScore += Random.Range(100, 500);

        UpdateScoreVisuals();
    }

    private void UpdateScoreVisuals()
    {
        LeanTween.value(gameObject, UpdateScoreText, score, targetScore, 1);
    }

    private void UpdateScoreText(float value)
    {
        score = (int)value;
        scoreText.text = score.ToString();
    }
    
}
