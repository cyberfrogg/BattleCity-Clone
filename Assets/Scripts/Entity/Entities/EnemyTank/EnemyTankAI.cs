using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class EnemyTankAI : EntityAI
    {
        private Vector2 _moveVector;
        [SerializeField] private EnemyTankAIState _state;
        private EnemyTankAIState _lastState;

        [SerializeField] private IdleEnemyTankAIState _idleState;
        [SerializeField] private MovingToPointEnemyTankAIState _movingToPointState;
        [SerializeField] private ShootingEnemyTankAIState _shootingState;
        private bool _isStateRunning;

        public override void Init(Entity entity)
        {
            base.Init(entity);

            _idleState.Init(Self);
            _movingToPointState.Init(Self);
            _shootingState.Init(Self);

            setNewAiState();
        }
        public override void UpdateAI()
        {
            base.UpdateAI();

            if(!_isStateRunning) setNewAiState();

            _idleState.UpdateState();
            _movingToPointState.UpdateState();
            _shootingState.UpdateState();
        }

        private void setNewAiState()
        {

            if (_isStateRunning) return;

            Array values = Enum.GetValues(typeof(EnemyTankAIState));

            EnemyTankAIState randomBar = 
                (EnemyTankAIState)values.GetValue(UnityEngine.Random.Range(0, values.Length));

            if(_lastState == randomBar)
            {
                _state = _state = EnemyTankAIState.Idle;
            }
            else
            {
                _state = randomBar;
            }

            _lastState = _state;

            controllTankStateSwitch();
        }

        private void controllTankStateSwitch()
        {
            _isStateRunning = true;

            switch (_state)
            {
                case EnemyTankAIState.Idle:
                    RunIdleState();
                    break;
                case EnemyTankAIState.MovingToPoint:
                    RunMovingToPointState();
                    break;
                case EnemyTankAIState.Shooting:
                    RunShootingState();
                    break;
                default:
                    RunIdleState();
                    break;
            }
        }

        public virtual void RunIdleState()
        {
            _idleState.RunState(OnStateEnd);
        }
        public virtual void RunMovingToPointState()
        {
            _movingToPointState.RunState(OnStateEnd);
        }
        public virtual void RunShootingState()
        {
            _shootingState.RunState(OnStateEnd);
        }

        public virtual void OnStateEnd()
        {
            _isStateRunning = false;
            //setNewAiState();
        }
    }

    public enum EnemyTankAIState
    {
        Idle,
        MovingToPoint,
        Shooting
    }
}
