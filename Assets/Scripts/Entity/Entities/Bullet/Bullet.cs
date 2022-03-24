using System;
using Cysharp.Threading.Tasks;
using GameUtils;
using UnityEngine;

namespace Entities
{
    public class Bullet : Entity
    {
        [HideInInspector] public Entity Owner;
        public int DamageCount = 1;

        [SerializeField] private float _bulletSpeed;
        private Vector2 _followDirection;
        private bool _isFollowing;

        public bool CanDestroySteel;
        public BulletEffect BulletEffect;
        public BulletType Type;
        

        public override void Start()
        {

            base.Start();
            //SetBulletDamage();
        }

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

            if (_isFollowing && !Game.Instance.IsGamePaused)
            {
                LookAt(_followDirection);
                transform.Translate((transform.right * -_followDirection.x + transform.up * _followDirection.y) * _bulletSpeed * Time.deltaTime);
            }

        }

        public override void OnThingCollidedEnter(Thing thing)
        {
            base.OnThingCollidedEnter(thing);


            if (Owner != null)
            {

                if (!thing.CompareTag("Water"))
                {
                    if (thing != Owner)
                    {
                        try
                        {
                            (thing as Tank).Damage(DamageCount, this);
                        }
                        catch
                        {

                        }

                        Instantiate(BulletEffect, transform.position, Quaternion.identity);
                        Destroy(gameObject);
                    }
                }

                
            }

            else
            {
                Instantiate(BulletEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        public void SetBulletDamage(int damage = 1)
        {
            DamageCount = damage;
        }

        public int GetBulletDamage()
        {
            return DamageCount;
        }

        
    }

    public enum BulletType
    {
        PlayerBullet,
        EnemyBullet
    }
}


