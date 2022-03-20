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
        [SerializeField]private int _tanksGeneratedPerWave;
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
            _tanksGeneratedPerWave = 0;
        }

        public override  void Update()
        {
            base.Update();

            /*if(WaveGeneration && _totalTankGeneration < Game.Instance._tanksToKill && _tanksGeneratedPerWave < _tanksPerWave)
            {
                GenerateTanks();
            }

            
            foreach (Tank item in _spawnedTanks)
            {
                if (item.IsDead)
                {
                    _spawnedTanks.Remove(item);
                    _tanksGeneratedPerWave--;

                    return;
                }
            }*/

            //if (_listOfRandomTank.Count > 0 && _tanksGeneratedPerWave ==0 && WaveGeneration)
            //{
            //    InitialTankGeneration();
            //}

            if (_listOfRandomTank.Count > 0 && _tanksGeneratedPerWave == 0 && WaveGeneration)
            {
                InitialTankGeneration();
            }

            foreach (Tank item in _spawnedTanks)
            {
                if (item.IsDead)
                {
                    _spawnedTanks.Remove(item);
                    _tanksGeneratedPerWave--;
                    GenerateTanks();
                    return;
                }
            }
        }


        [ContextMenu("Generate EnemyAnimation")]
        async UniTask GenerateEnemyAnimation(int count = 3)
        {


            _generateAnimationStop = true;

            if (count == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    _spawners[i].gameObject.GetComponent<Spawner>().AnimationObject.SetActive(true);
                }
            }
            else
            {
                _spawners[count].gameObject.GetComponent<Spawner>().AnimationObject.SetActive(true);

            }


            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

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

        private async void InitialTankGeneration()
        {

            _tanksGeneratedPerWave += 3;

            if (!_generateAnimationStop)
                await GenerateEnemyAnimation();


            for (int i = 0; i < 3; i++)
            {
                bool flashyTank = false;
                int getTankId = GetRandomTank();
                _totalTankGeneration++;

                if (_totalTankGeneration == 4 || _totalTankGeneration == 11 || _totalTankGeneration == 18)
                {
                    flashyTank = true;
                }
                Tank spawnedTank = _spawners[i].Spawn(_listOfRandomTank[getTankId], flashyTank) as Tank;


                RemoveTankFromList(getTankId);

                _spawnedTanks.Add(spawnedTank);
            }



        }

        private async void GenerateTanks()
        {

            /*int loopCount = _tanksPerWave - _spawnedTanks.Count;
            _tanksGeneratedPerWave += loopCount;
            
            if(_tanksGeneratedPerWave > _tanksPerWave)
                return;

            if (!_generateAnimationStop)
                await GenerateEnemyAnimation(loopCount);


            if (_generateWave)
            {
                for (int i = 0; i < loopCount; i++)
                {
                    bool flashyTank = false;
                    int getTankId = GetRandomTank();
                    _totalTankGeneration++;

                    if (_totalTankGeneration == 4 || _totalTankGeneration == 11 || _totalTankGeneration == 18)
                    {
                        flashyTank = true;
                    }
                    Tank spawnedTank = _spawners[i].Spawn(_listOfRandomTank[getTankId], flashyTank) as Tank;


                    RemoveTankFromList(getTankId);

                    _spawnedTanks.Add(spawnedTank);
                }
                _waveCount++;
                _generateWave = false;
                _generateAnimationStop = false;
            }*/



            if (_listOfRandomTank.Count > 0)
            {
                int random = Random.Range(0, 3);
                _tanksGeneratedPerWave++;

                Debug.Log("Random spawner : " + random);

                //if (!_generateAnimationStop)
                await GenerateEnemyAnimation(random);

                bool flashyTank = false;
                int getTankId = GetRandomTank();
                _totalTankGeneration++;

                if (_totalTankGeneration == 4 || _totalTankGeneration == 11 || _totalTankGeneration == 18)
                {
                    flashyTank = true;
                }

                Tank spawnedTank = _spawners[random].Spawn(_listOfRandomTank[getTankId], flashyTank) as Tank;


                RemoveTankFromList(getTankId);

                _spawnedTanks.Add(spawnedTank);

                _generateWave = false;
                _generateAnimationStop = false;
            }
            

        }
    }
}