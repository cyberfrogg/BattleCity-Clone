using GameUtils;
using UnityEngine;

namespace SceneEvents
{
    public class OnPlayerTankKilled : SceneEvent
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private float _playerSpawnTime = 3000;


        public override void TriggerEvent()
        {
            base.TriggerEvent();


            if (Game.Instance.StatisticsCollector.Statistics.LevelRemainingPlayerTanksCount > 0)
            {
                _spawner.Spawn(_playerSpawnTime);
            }
            
        }


    }
}
