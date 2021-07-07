using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class Bullet : Entity
    {
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

        public override void Update()
        {
            base.Update();

            if (_isFollowing)
            {
                LookAt(_followDirection);
                transform.Translate((transform.right * -_followDirection.x + transform.up * _followDirection.y) * _bulletSpeed * Time.deltaTime);
            }
        }
    }
}
