using Blocks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class Bullet : Entity
    {
        public Entity Owner;

        [SerializeField] private float _bulletSpeed;
        private Vector2 _followDirection;
        private bool _isFollowing;

        /// <summary>
        /// Stars bullet to follow direction
        /// </summary>
        /// <param name="followDirection"></param>
        public virtual void Follow(Vector2 followDirection)
        {
            _isFollowing = true;
            _followDirection = followDirection;
        }

        /// <summary>
        /// Stars bullet to follow direction with owner
        /// </summary
        /// <param name="followDirection">Bullet move direction</param>
        /// <param name="owner">Owner of bullet</param>
        public virtual void Follow(Vector2 followDirection, Entity owner)
        {
            _isFollowing = true;
            _followDirection = followDirection;
            Owner = owner;
        }

        public override void Update()
        {
            base.Update();

            if (_isFollowing)
            {
                LookAt(_followDirection);
                transform.Translate((transform.right * -_followDirection.x + transform.up * _followDirection.y) * _bulletSpeed * Time.deltaTime);
            }
        }

        public override void OnThingCollidedEnter(Thing thing)
        {
            base.OnThingCollidedEnter(thing);

            if(Owner != null)
            {
                if(thing != Owner)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
