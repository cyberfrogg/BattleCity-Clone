using UnityEngine;
using Statistics;

namespace SceneEvents
{
    public class OnTankKilledSceneEvent : SceneEvent
    {
        [SerializeField] private LevelStatisticsCollector _collector;

        public override void TriggerEvent()
        {
            base.TriggerEvent();

            _collector.OnTankKilled();
        }
    }
}
