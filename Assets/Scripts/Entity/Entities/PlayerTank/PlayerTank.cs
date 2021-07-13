using GameUtils;
using UnityEngine;

namespace Entities
{
    public class PlayerTank : Tank
    {
        [SerializeField] private PlayerTankAI _playerTankAI;

        public override void Awake()
        {
            base.Awake();

            AI = _playerTankAI;
            AI.Init(this);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Die(Entity deathOwner)
        {
            Game.Instance.Triggers.OnPlayerKilled.Invoke();

            base.Die(deathOwner);
        }
    }
}
