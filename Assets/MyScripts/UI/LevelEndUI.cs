using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndUI : MonoBehaviour
{
    [SerializeField] private string _nextLevelScene;
    [SerializeField] private int _nextLevelDelay = 5000;

    [SerializeField] private TextMeshProUGUI _highScore;
    [SerializeField] private TextMeshProUGUI _stageNo;
    [SerializeField] private TextMeshProUGUI _playerScore;
    [SerializeField] private TextMeshProUGUI _basicTankScore;
    [SerializeField] private TextMeshProUGUI _fastTankScore;
    [SerializeField] private TextMeshProUGUI _powerTankScore;
    [SerializeField] private TextMeshProUGUI _armorTankScore;
    [SerializeField] private TextMeshProUGUI _totalTankScore;

    [SerializeField] private TextMeshProUGUI _basicTankTotalScore;
    [SerializeField] private TextMeshProUGUI _fastTankTotalScore;
    [SerializeField] private TextMeshProUGUI _powerTankTotalScore;
    [SerializeField] private TextMeshProUGUI _armorTankTotalScore;


    public AudioClip _calculationSound;
    public void Initialization()
    {
        LevelEndCalculation();
    }

    private async void LevelEndCalculation()
    {
        _highScore.text = PlayerPrefs.GetInt("HighScore").ToString("00000");
        _stageNo.text = (PlayerPrefs.GetInt("StageCount") - 1).ToString("00");
        _playerScore.text = PlayerPrefs.GetInt("TotalScore").ToString();


        await CalculateEachTank(EnemyTankType.Basic, _basicTankScore , _basicTankTotalScore);
        await CalculateEachTank(EnemyTankType.Fast, _fastTankScore , _fastTankTotalScore);
        await CalculateEachTank(EnemyTankType.Power, _powerTankScore, _powerTankTotalScore);
        await CalculateEachTank(EnemyTankType.Armor, _armorTankScore, _armorTankTotalScore);

        await CalculateTotalTank();

        LoadNextLevel();
    }

    private async UniTask CalculateEachTank(EnemyTankType tankType, TextMeshProUGUI scoreText , TextMeshProUGUI totalScoreText)
    {
        scoreText.text = "0";
        totalScoreText.text = "0";
        AudioManager.Instance.PlaySFX(_calculationSound);
        await UniTask.Delay(TimeSpan.FromSeconds((_calculationSound.length)));

        for (int i = 1; i <= DestroyedTank.instance.TankTypeDestroyed[tankType]; i++)
        {
            scoreText.text = i.ToString("00");

            totalScoreText.text = (((int) tankType + 1) * 100 * i).ToString();
            AudioManager.Instance.PlaySFX(_calculationSound);
            await UniTask.Delay(TimeSpan.FromSeconds((_calculationSound.length / 2)));
        }
    }

    private async UniTask CalculateTotalTank()
    {
        int tempScore = 0;
        foreach (var item in DestroyedTank.instance.TankTypeDestroyed)
        {
            tempScore += item.Value;
        }

        _totalTankScore.text = tempScore.ToString("00");

    }

    private async void LoadNextLevel()
    {
        await Task.Delay(_nextLevelDelay);
        SceneManager.LoadScene(_nextLevelScene);
    }
}
