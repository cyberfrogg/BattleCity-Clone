using System;
using Cysharp.Threading.Tasks;
using Entities;
using UnityEngine;

namespace Guns
{
    /// <summary>
    /// Default Player Gun
    /// </summary>
    public class PlayerGun : Gun
    {
        [SerializeField] private float _shootOffsetDistance;
        public  float FiringRate;
        private float _nextFiring;
        [SerializeField]private int _gunTier;
        [SerializeField] private int _firingCount;
        [SerializeField] private bool _destroySteel;


        public override void Update()
        {
            base.Update();
            Debug.Log($"Can destroy for player :  {_bulletPrefab.CanDestroySteel}");
            _nextFiring -= Time.deltaTime;
        }

        public override async void Shoot()
        {
            if (_nextFiring <= 0)
            {
                _nextFiring = FiringRate;
                for (int i = 0; i < _firingCount; i++)
                {
                    Bullet bullet = SpawnBullet();
                    bullet.transform.position = Tank.transform.position + (Tank.transform.up * _shootOffsetDistance);
                    bullet.Follow(Tank.transform.up, Tank);
                    bullet.CanDestroySteel = _destroySteel;
                    await UniTask.Delay(TimeSpan.FromSeconds(.2f));
                }
            }

        }


        [ContextMenu("Change Gun Tier")]
        public void ChangeTier()
        {
            _gunTier++;
            ChangeGunPower(_gunTier);
        }

        private void ChangeGunPower(int tier)
        {
            if (tier == 1)
            {
                FiringRate = 1f;
            }

            if (tier == 2)
            {
                _firingCount = 2;
            }

            if (tier == 3)
            {
                _destroySteel = true;
            }
        }
    }
}