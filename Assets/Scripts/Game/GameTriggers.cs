using System;
using UnityEngine.Events;

namespace GameUtils
{
    /// <summary>
    /// All key-event links
    /// </summary>
    [Serializable]
    public class GameTriggers
    {
        public UnityEvent OnCityBlockDied;
        public UnityEvent OnTankKilled;
        public UnityEvent OnPlayerKilled;
        public UnityEvent OnPlayerTanksEnd;
        public UnityEvent OnLevelDone;
    }
}