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
        [SerializeField]private int _waveCount = 1;

        private List<int> _listOfRandomTank = new List<int>();
        private int _totalTankGeneration;
        private List<int> _tankPowerUpTime = new List<int>();

        public override void Start()
        {
            base.Start();
            SpawnRandomTanks();
            _totalTankGeneration = 0;

            
           
        }

        public override  void Update()
        {
            base.Update();

            if(_spawnedTanks.Count <=0 && WaveGeneration && _totalTankGeneration < Game.Instance._tanksToKill)
            {

                if (!_generateAnimationStop)
                    GenerateEnemyAnimation();

                if (_generateWave)
                {
                    for (int i = 0; i < _tanksPerWave; i++)
                    {
                        bool flashyTank = false;
                        int GetTankId = GetRandomTank();
                        _totalTankGeneration++;

                        if (_totalTankGeneration == 4 || _totalTankGeneration == 11 || _totalTankGeneration == 18)
                        {
                            flashyTank = true;
                        }
                        Tank spawnedTank = _spawners[i].Spawn(_listOfRandomTank[GetTankId] , flashyTank) as Tank;


                        RemoveTankFromList(GetTankId);
                        
                        _spawnedTanks.Add(spawnedTank);
                    }

                    _waveCount++;
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

        [ContextMenu("Destroy Waves")]
        public void DestroyWave()
        {
            foreach (var tank in _spawnedTanks)
            {
                Game.Instance.Triggers.OnTankKilled.Invoke(0);
                Destroy(tank.gameObject);
            }

            _spawnedTanks.Clear();
        }

        public void SpawnRandomTanks()
        {
            int levelId = PlayerPrefs.GetInt("StageCount");
            
            int armorTankCount = Mathf.Clamp(Mathf.FloorToInt(Random.Range(.2f,.6f)  * levelId), 0, 10);
            int fastTankCount = Mathf.Clamp(Mathf.FloorToInt(Random.Range(.6f, .8f) * levelId), 0, 15);
            int powerTankCount = Mathf.Clamp(Mathf.FloorToInt(Random.Range(.8f, .9f) * levelId), 0, 5);
            int basicTankCount = Mathf.Abs(Game.Instance._tanksToKill - armorTankCount + powerTankCount + fastTankCount);



            for (int i = 0; i < armorTankCount; i++)
            {
                _listOfRandomTank.Add(3);
            }

            for (int i = 0; i < powerTankCount; i++)
            {
                _listOfRandomTank.Add(2);
            }

            for (int i = 0; i < fastTankCount; i++)
            {
                _listOfRandomTank.Add(1);
            }

            for (int i = 0; i < basicTankCount; i++)
            {
                _listOfRandomTank.Add(0);
            }

            Debug.Log("Spawned tank count "+ _listOfRandomTank.Count );

            if (_listOfRandomTank.Count - Game.Instance._tanksToKill > 0)
            {
                _listOfRandomTank.RemoveRange(Game.Instance._tanksToKill, _listOfRandomTank.Count - Game.Instance._tanksToKill);
            }

            Debug.Log("Spawned tank count " + _listOfRandomTank.Count);

        }

        public int GetRandomTank()
        {
            if (_listOfRandomTank.Count > 1)
            {
                return _listOfRandomTank[Random.Range(0, _listOfRandomTank.Count)];
            }

            return 0;

        }

        public void RemoveTankFromList(int id)
        {
            
            _listOfRandomTank.RemoveAt(id);

            Debug.Log("Spawned tank count " + _listOfRandomTank.Count);
        }
    }
}