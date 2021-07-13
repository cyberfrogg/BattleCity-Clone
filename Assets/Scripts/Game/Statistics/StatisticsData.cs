
using UnityEngine;

namespace Statistics
{
    /// <summary>
    /// Main data struct of statistics
    /// </summary>
    [SerializeField]
    public class StatisticsData
    {
        public int TotalScore;
        public int TanksKilled;

        public int LevelScore;
        public int LevelRemainingPlayerTanksCount = 12;
    }
}
