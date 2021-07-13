using System;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    [Serializable]
    public class IdleEnemyTankAIState : EntityAIState
    {
        [SerializeField] private float _idleTimeMin;
        [SerializeField] private float _idleTimeMax;
        private float _realIdleTime;
        private bool _runTimer;
        private float _runTimerTime;

        public override void RunState(UnityAction callbackevent)
        {
            base.RunState(callbackevent);

            _realIdleTime = UnityEngine.Random.Range(_idleTimeMin, _idleTimeMax);

            _runTimer = true;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (IsActive)
            {
                if (_runTimer)
                {
                    _runTimerTime += 1f * Time.deltaTime;

                    if(_runTimerTime >= _realIdleTime)
                    {
                        resetIdleTimer();
                        StopState();
                    }
                }
            }
        }

        private void resetIdleTimer()
        {
            _runTimer = false;
            _runTimerTime = 0;
        }
    }
}
