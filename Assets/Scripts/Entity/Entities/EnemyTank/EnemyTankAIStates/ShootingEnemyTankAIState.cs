using System;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    [Serializable]
    public class ShootingEnemyTankAIState : EntityAIState
    {
        [SerializeField] private float _idleDelay;
        private bool _runTimer;
        private float _runTimerTime;

        public override void RunState(UnityAction callbackevent)
        {
            base.RunState(callbackevent);

            Tank selfTank = Self as Tank;
            selfTank.Gun.Shoot();

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

                    if (_runTimerTime >= _idleDelay)
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
