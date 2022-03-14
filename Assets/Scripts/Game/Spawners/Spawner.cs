using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

namespace GameUtils
{
    /// <summary>
    /// Base class of spawning things
    /// </summary>
    public class Spawner : Thing
    {
        public Thing[] _spawnObject;

        [SerializeField] private Transform _spawnPoint;
        public GameObject AnimationObject;
        public Thing Tank;
        private SpawnerTank _spawnerTank;


        /// <summary>
        /// Spawns thing
        /// </summary>
        public virtual Thing Spawn(int tankId = 0)
        {
            //_spawnerTank = new SpawnerTank();
            //int level = GetTier();
            //Debug.Log("LevelCount ==> "+level);
            tankId = Mathf.Clamp(tankId, 0, _spawnObject.Length-1);

            //level = Mathf.Clamp(level , 0 ,_spawnObject.Length-1);


            Thing instance = Instantiate((Thing)_spawnObject[tankId].Clone());


            instance.transform.position = _spawnPoint != null ? _spawnPoint.position : transform.position;


            return instance;
        }

        /// <summary>
        /// Spawns thing with animation and delay 
        /// </summary>
        /// <param name="animationTime">Delay (in ms)</param>
        public virtual async void Spawn(float animationTime)
        {

            AnimationObject.SetActive(true);

            await Task.Delay((int)animationTime);

            AnimationObject.SetActive(false);

            Tank = Spawn();
        }


    }
}
