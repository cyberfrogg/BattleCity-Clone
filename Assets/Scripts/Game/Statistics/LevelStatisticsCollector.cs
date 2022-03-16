using System;
using GameUtils;
using UnityEngine;

namespace Statistics
{
    /// <summary>
    /// Collects statistics of Level e.g. tanks killed and other scores
    /// </summary>
    public class LevelStatisticsCollector : Thing
    {
        
        public StatisticsData Statistics { get; private set; } = new StatisticsData();

        [SerializeField] private StatisticsDisplay _display;
        [SerializeField]private int _previousScore = 0;
        [SerializeField] private int _currentLevel;


        public override void Awake()
        {
            base.Awake();
            
            
            Statistics.TotalScore =  GetTotalScore();
            Statistics.LevelRemainingPlayerTanksCount = GetPlayerLife();
            _previousScore = Statistics.TotalScore;

            _display.UpdateDisplay(Statistics);

            
        }


        public override void Start()
        {
            _currentLevel = GetStageLevel();
        }

        public void OnTankKilled(int score)
        {
            Debug.Log("called score : "+ score);
            Statistics.TanksKilled++;
            UpdateScore(score);
        }

        public void OnPlayerKilled()
        {
            Statistics.LevelRemainingPlayerTanksCount--;

            _display.UpdateDisplay(Statistics);

            if(Statistics.LevelRemainingPlayerTanksCount <= 0)
            {
                SetHighScore();
                Game.Instance.Triggers.OnPlayerTanksEnd.Invoke();
            }
        }

        public void IncreaseLife()
        {
            Statistics.LevelRemainingPlayerTanksCount++;
            _display.UpdateDisplay(Statistics);
            PlayerPrefs.SetInt("PlayerLifeCount" , Statistics.LevelRemainingPlayerTanksCount);
        }

        public void UpdateTotalScore()
        {
            PlayerPrefs.SetInt("TotalScore" , Statistics.TotalScore);

            if (PlayerPrefs.HasKey("TotalScore"))
            {
                Debug.Log("Update total score is avilable");
            }

        }

        private int GetTotalScore()
        {

            if (PlayerPrefs.HasKey("TotalScore"))
            {
                return PlayerPrefs.GetInt("TotalScore");
            }
            else
            {
                return 0;
            }

           

        }

        private int GetPlayerLife()
        {
            if (PlayerPrefs.HasKey("PlayerLifeCount"))
            {
                return PlayerPrefs.GetInt("PlayerLifeCount");
            }

            return 1;
        }

        private void UpdateScore(int score)
        {
            Statistics.LevelScore += 1 * score;

            Statistics.TotalScore += 1 * score;


            UpdateTotalScore();
            

            _display.UpdateDisplay(Statistics);

            ValidateScore();
        }

        private void ValidateScore()
        {
            
            if (Statistics.TotalScore >  _previousScore + 1000)
            {
                _previousScore += 1000;
                IncreaseLife();
            }
            
        }

        public void SetHighScore()
        {
            if (PlayerPrefs.HasKey("HighScore"))
            {
                if(PlayerPrefs.GetInt("HighScore") < Statistics.TotalScore)
                    PlayerPrefs.SetInt("HighScore",Statistics.TotalScore);
            }
            else
            {
                PlayerPrefs.SetInt("HighScore", Statistics.TotalScore);
            }
        }

        public void SetStageLevel()
        {
            if (PlayerPrefs.HasKey("StageCount"))
            {
                PlayerPrefs.SetInt("StageCount", (PlayerPrefs.GetInt("StageCount")+1));
            }
            else
            {
                PlayerPrefs.SetInt("StageCount", 1);
            }
        }

        public int GetStageLevel()
        {
            if (!PlayerPrefs.HasKey("StageCount"))
            {
                PlayerPrefs.SetInt("StageCount", 1);
            }

            return PlayerPrefs.GetInt("StageCount");
        }

    }
}
