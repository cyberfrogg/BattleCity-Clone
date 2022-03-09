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

        [SerializeField] private int _tanksToKill = 10;

        public override void Awake()
        {
            base.Awake();

            Instance = this;
            Triggers.OnTankKilled.AddListener(validateDoneScore);
        }

        private void validateDoneScore(int score)
        {
            StatisticsCollector.OnTankKilled(score);

            if(StatisticsCollector.Statistics.TanksKilled >= _tanksToKill)
            {
                Triggers.OnLevelDone.Invoke();
            }
        }
    }
}
