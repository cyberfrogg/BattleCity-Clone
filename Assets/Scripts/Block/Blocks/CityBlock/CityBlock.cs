using Entities;
using UnityEngine;
using GameUtils;

namespace Blocks
{
    public class CityBlock : Block
    {
        [SerializeField] private Sprite _deadSprite;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private int _maxHealth = 1;
        private int _currentHealth;
        private bool _isDied;

        public override void Start()
        {
            base.Start();
            _currentHealth = _maxHealth;
        }

        public override void OnThingCollidedEnter(Thing thing)
        {
            base.OnThingCollidedEnter(thing);

            if(thing is Bullet)
            {
                Bullet bullet = thing as Bullet;
                Damage(bullet.DamageCount);
            }
        }

        public void Damage(int damage)
        {
            _currentHealth -= damage;
            validateHealth();
        }
        private void validateHealth()
        {
            if (_currentHealth <= 0)
            {
                if (!_isDied)
                {
                    _isDied = true;
                    Game.Instance.Triggers.OnCityBlockDied.Invoke();
                    _spriteRenderer.sprite = _deadSprite;
                }
            }
        }
    }
}