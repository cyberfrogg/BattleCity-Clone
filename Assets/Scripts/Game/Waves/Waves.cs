using System;
using Entities;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameUtils
{
    public class Waves : Thing
    {
        [SerializeField] private Spawner[] _spawners;
        [SerializeField] private int _tanksPerWave;
        public bool WaveGeneration;
        [SerializeField]private bool _generateWave;
        [SerializeField]private bool _generateAnimationStop;

        [SerializeField]private List<Tank> _spawnedTanks = new List<Tank>();

        public override void Start()
        {
            base.Start();
        }

        public override  void Update()
        {
            base.Update();

            if(_spawnedTanks.Count <= 0 && WaveGeneration)
            {
                if(!_generateAnimationStop)
                    GenerateEnemyAnimation();

                if (_generateWave)
                {
                    for (int i = 0; i < _tanksPerWave; i++)
                    {
                        Tank spawnedTank = _spawners[i].Spawn() as Tank;
                        _spawnedTanks.Add(spawnedTank);
                    }

                    _generateWave = false;
                    _generateAnimationStop = false;
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


        [ContextMenu("Generate Enemy")]
        async void GenerateEnemyAnimation()
        {
            _generateAnimationStop = true;

            foreach (var enemy in _spawners)
            {
                enemy.gameObject.GetComponent<Spawner>().AnimationObject.SetActive(true);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), ignoreTimeScale: false);

            foreach (var enemy in _spawners)
            {
                enemy.gameObject.GetComponent<Spawner>().AnimationObject.SetActive(false);
            }

            _generateWave = true;

        }
    }
}