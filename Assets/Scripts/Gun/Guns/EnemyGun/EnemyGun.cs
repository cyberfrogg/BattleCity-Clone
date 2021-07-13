using Entities;
using UnityEngine;

namespace Guns
{
    /// <summary>
    /// Default Player Gun
    /// </summary>
    public class EnemyGun : Gun
    {
        [SerializeField] private float _shootOffsetDistance;

        public override void Shoot()
        {
            Bullet bullet = SpawnBullet();
            bullet.transform.position = Tank.transform.position + (Tank.transform.up * _shootOffsetDistance);
            bullet.Follow(Tank.transform.up, Tank);
        }
    }
}