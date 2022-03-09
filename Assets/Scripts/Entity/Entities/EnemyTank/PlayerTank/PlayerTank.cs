using GameUtils;
using UnityEngine;

namespace Entities
{
    public class PlayerTank : Tank
    {
        [SerializeField] private PlayerTankAI _playerTankAI;
        [SerializeField] private ArrowPlayerTankController _arrowPlayerTankController;
        public bool IsKeyboard;



        public override void Awake()
        {
            base.Awake();

            if (IsKeyboard)
                AI = _playerTankAI;
            else
                AI = _arrowPlayerTankController;
            
            AI.Init(this);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Damage(int damageCount, Entity damageOwner)
        {
            if(!_powerUps.PickedHelmet)
                base.Damage(damageCount, damageOwner);
        }

        public override void Die(Entity deathOwner)
        {
            Game.Instance.Triggers.OnPlayerKilled.Invoke();

            base.Die(deathOwner);
        }
    }
}
