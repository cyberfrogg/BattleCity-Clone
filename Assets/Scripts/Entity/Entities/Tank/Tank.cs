using Guns;
using System;
using UnityEngine;
using GameUtils;

namespace Entities
{
    /// <summary>
    /// Base class of Tank Entity
    /// </summary>
    public class Tank : Entity
    {
        public Gun Gun
        {
            get
            {
                return _gun;
            }
            set
            {
                _gun = value;
                _gun.Init(this);
            }
        }
        public float MoveSpeed;
        public int Health { get; private set; }
        public bool IsDead { get; private set; }

        [SerializeField] private int _maxHealth;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _spriteContainer;
        [SerializeField] private Gun _gun;

        public override void Awake()
        {
            base.Awake();

            Health = _maxHealth;

            if (_gun != null)
            {
                _gun.Init(this);
            }
        }

        /// <summary>
        /// Moves self (tank)
        /// </summary>
        /// <param name="moveVector">Normolized movement vector</param>
        public virtual void Move(Vector2 moveVector)
        {
            _rigidbody.velocity = (moveVector * MoveSpeed);

            if (moveVector.x != 0 || moveVector.y != 0)
            {
                LookAt(moveVector);
            }
        }

        /// <summary>
        /// Damages tank
        /// </summary>
        /// <param name="damageCount">Count of Damage. Must be greated than 0</param>
        /// <param name="damageOwner">Owner</param>
        public virtual void Damage(int damageCount, Entity damageOwner)
        {
            if(damageCount <= 0)
            {
                throw new ArgumentException("Damage must be greater than 0");
            }

            Health -= damageCount;

            ValidateHealth(damageOwner);
        }

        /// <summary>
        /// Validating health. Health validator
        /// </summary>
        /// <param name="validateOwner"></param>
        public virtual void ValidateHealth(Entity validateOwner)
        {
            if (Health <= 0)
            {
                Die(validateOwner);
            }
        }

        /// <summary>
        /// Death. Dieing
        /// </summary>
        /// <param name="deathOwner"></param>
        public virtual void Die(Entity deathOwner)
        {
            IsDead = true;

            Destroy(gameObject);
        }

    }
}
