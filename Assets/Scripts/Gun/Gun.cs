using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Guns
{
    /// <summary>
    /// Base class of Gun
    /// </summary>
    public class Gun : ScriptableObject, ICloneable
    {
        [SerializeField] private Bullet _bulletPrefab;

        protected Tank _tank;

        /// <summary>
        /// Initializinng gun
        /// </summary>
        /// <param name="tank">Self tank</param>
        public virtual void Init(Tank tank)
        {
            _tank = tank;
        }

        public virtual void Shoot()
        {
            Bullet bullet = SpawnBullet();
            bullet.transform.position = _tank.transform.position;
            bullet.Follow(_tank.transform.up, _tank);
        }

        public virtual Bullet SpawnBullet()
        {
            Bullet bullet = Instantiate((Bullet)_bulletPrefab.Clone());
            bullet.transform.position = _tank.transform.position;

            return bullet;
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
