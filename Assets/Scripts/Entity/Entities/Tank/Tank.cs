using Guns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _spriteContainer;
        [SerializeField] private Gun _gun;

        public override void Start()
        {
            base.Start();

            if(_gun != null)
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

    }
}
