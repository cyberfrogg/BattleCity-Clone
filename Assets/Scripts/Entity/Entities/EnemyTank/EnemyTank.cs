using GameUtils;
using JetBrains.Annotations;
using UnityEngine;

namespace Entities
{
    public class EnemyTank : Tank
    {
        [SerializeField] private EnemyTankAI _enemyTankAI;
        [SerializeField] private int _killingValue;
        public EnemyTankType _enemyType;

        private SpriteRenderer _spriteRenderer;
        [CanBeNull] public Sprite[] DamagedTankSprite;
        [CanBeNull] public AudioClip DamagedTankAudio;
        private int _spriteIteration;

        public GameObject TankDestroyEffect;

        



        public override void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            base.Awake();
            AI = _enemyTankAI;

            AI.Init(this);
        }

        public override void Update()
        {
            base.Update();
            if (_powerUps.Timer)
            {
                StopMoving();
            }

        }

        public override void Move(Vector2 moveVector)
        {
            Debug.Log("Called from enemytank");
            base.Move(moveVector);
        }

        public override void ValidateHealth(Entity validateOwner)
        {

            if (_enemyType == EnemyTankType.Armor)
            {
                if (Health > 0)
                {
                    AudioManager.Instance.PlaySFX(DamagedTankAudio);
                }

                if (Health <= _maxHealth / 2)
                {
                    _spriteRenderer.sprite = DamagedTankSprite[_spriteIteration];
                    _spriteIteration++;
                    _spriteIteration = Mathf.Clamp(_spriteIteration, 0, DamagedTankSprite.Length-1);
                }
            }

           

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
            AudioManager.Instance.PlaySFX(TankDestroySfx);
            GameObject go = Instantiate(TankDestroyEffect, transform.position, Quaternion.identity);
            go.GetComponent<ShowScore>().SetTankScore(_enemyType);

            DestroyedTank.instance.TankTypeDestroyed[_enemyType] += 1;

            Game.Instance.Triggers.OnTankKilled.Invoke(_killingValue);

            Debug.Log( _enemyType + " killed " + DestroyedTank.instance.TankTypeDestroyed[_enemyType]);

            base.Die(deathOwner);
        }


    }

    
}
