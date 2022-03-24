using UnityEngine;

namespace Statistics
{
    /// <summary>
    /// Main data struct of statistics
    /// </summary>
    [System.Serializable]
    public class StatisticsData
    {
        public int TotalScore;
        public int TanksKilled;

        public int LevelScore;
        public int LevelRemainingPlayerTanksCount;
    }
}
