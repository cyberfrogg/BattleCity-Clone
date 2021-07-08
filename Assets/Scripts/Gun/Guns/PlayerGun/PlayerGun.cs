using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Guns
{
    [CreateAssetMenu(fileName = "PlayerGun", menuName = "Guns/PlayerGun", order = 1)]


    /// <summary>
    /// Default Player Gun
    /// </summary>
    public class PlayerGun : Gun
    {
        [SerializeField] private float _shootOffsetDistance;

        public override void Shoot()
        {
            Bullet bullet = SpawnBullet();
            bullet.transform.position = _tank.transform.position + (_tank.transform.up * _shootOffsetDistance);
            bullet.Follow(_tank.transform.up, _tank);
        }
    }
}