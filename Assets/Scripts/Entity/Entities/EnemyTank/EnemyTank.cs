using GameUtils;
using UnityEngine;

namespace Entities
{
    public class EnemyTank : Tank
    {
        [SerializeField] private EnemyTankAI _enemyTankAI;

        public override void Awake()
        {
            base.Awake();

            AI = _enemyTankAI;
            AI.Init(this);
        }

        public override void ValidateHealth(Entity validateOwner)
        {
            if (Health <= 0)
            {
                if(validateOwner != null)
                {
                    if((validateOwner as Bullet).Owner != null)
                    {
                        if ((validateOwner as Bullet).Owner is PlayerTank)
                        {
                            Die((validateOwner as Bullet).Owner);
                        }
                    }
                }
            }
        }

        public override void Die(Entity deathOwner)
        {
            Game.Instance.Triggers.OnTankKilled.Invoke();

            base.Die(deathOwner);
        }
    }
}
