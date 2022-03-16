using System;
using System.Collections.Generic;
using UnityEngine;
using Statistics;

namespace GameUtils
{
    /// <summary>
    /// Main class of game. Contains Links to individual classes on scene. Game.Instance to get instance of Game 
    /// </summary>
    public class Game : Thing
    {
        public static Game Instance;

        public GameTriggers Triggers;
        public Map Map;
        public LevelStatisticsCollector StatisticsCollector;

        public int _tanksToKill;
        public bool isLevelDone;

        public override void Awake()
        {
            base.Awake();
            
            Instance = this;
            IncreaseTankPerStage();
            Triggers.OnTankKilled.AddListener(validateDoneScore);
        }

        public override void Start()
        {
            
        }

        private void validateDoneScore(int score)
        {
            StatisticsCollector.OnTankKilled(score);

            if(StatisticsCollector.Statistics.TanksKilled >= _tanksToKill)
            {
                StatisticsCollector.UpdateTotalScore();
                StatisticsCollector.SetStageLevel();
        

                Triggers.OnLevelDone.Invoke();
            }
        }

        public void IncreaseTankPerStage()
        {

            if (!PlayerPrefs.HasKey("StageCount"))
            {
                PlayerPrefs.SetInt("StageCount", 1);
            }

           
            _tanksToKill = Mathf.Clamp((PlayerPrefs.GetInt("StageCount")) * 3, 3, 30);
            
        }

    }
}
