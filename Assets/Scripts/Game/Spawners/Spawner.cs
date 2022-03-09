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
        


        /// <summary>
        /// Spawns thing
        /// </summary>
        public virtual Thing Spawn(int waveCount = 1)
        {

            waveCount = Mathf.Clamp(waveCount, 1, _spawnObject.Length);

            Thing instance = Instantiate((Thing)_spawnObject[Random.Range(0,waveCount)].Clone());


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
