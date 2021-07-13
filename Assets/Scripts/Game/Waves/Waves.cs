using Entities;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtils
{
    public class Waves : Thing
    {
        [SerializeField] private Spawner[] _spawners;
        [SerializeField] private int _tanksPerWave;

        private List<Tank> _spawnedTanks = new List<Tank>();

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();

            if(_spawnedTanks.Count <= 0)
            {
                for (int i = 0; i < _tanksPerWave; i++)
                {
                    Tank spawnedTank = _spawners[Random.Range(0, _spawners.Length)].Spawn() as Tank;
                    _spawnedTanks.Add(spawnedTank);
                }

            }

            foreach (Tank item in _spawnedTanks)
            {
                if (item.IsDead)
                {
                    _spawnedTanks.Remove(item);
                    return;
                }
            }
        }
    }
}