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
        public Thing _spawnObject;

        [SerializeField] private Transform _spawnPoint;
        public GameObject AnimationObject;
        public Thing Tank;
        


        /// <summary>
        /// Spawns thing
        /// </summary>
        public virtual Thing Spawn()
        {

            Thing instance = Instantiate((Thing)_spawnObject.Clone());


            instance.transform.position = _spawnPoint != null ? _spawnPoint.position : transform.position;


            return instance;
        }

        /// <summary>
        /// Spawns thing with animation and delay 
        /// </summary>
        /// <param name="animationTime">Delay (in ms)</param>
        public virtual async void Spawn(int animationTime)
        {

            AnimationObject.SetActive(true);

            await Task.Delay(animationTime);

            AnimationObject.SetActive(false);

            Tank = Spawn();
        }

    }
}
