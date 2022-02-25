using GameUtils;
using UnityEngine;

namespace SceneEvents
{
    public class OnPlayerTankKilled : SceneEvent
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private int _playerSpawnTime = 3000;


        public override void TriggerEvent()
        {
            base.TriggerEvent();

            _spawner.Spawn(_playerSpawnTime);
        }


    }
}
