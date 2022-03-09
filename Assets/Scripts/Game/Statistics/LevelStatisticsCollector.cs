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
        [SerializeField] private int _tankKillModifier = 10;

        public override void Awake()
        {
            base.Awake();

            Statistics.LevelRemainingPlayerTanksCount = 5;
            
        }

        public void OnTankKilled(int score)
        {
            Debug.Log("called score : "+ score);
            Statistics.TanksKilled++;
            Statistics.LevelScore += 1 * score;

            _display.UpdateDisplay(Statistics);
        }

        public void OnPlayerKilled()
        {
            Statistics.LevelRemainingPlayerTanksCount--;

            _display.UpdateDisplay(Statistics);

            if(Statistics.LevelRemainingPlayerTanksCount <= 0)
            {
                Game.Instance.Triggers.OnPlayerTanksEnd.Invoke();
            }
        }

        public void IncreaseLife()
        {
            Statistics.LevelRemainingPlayerTanksCount++;
        }
    }
}
