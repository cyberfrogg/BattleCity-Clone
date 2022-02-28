using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameUtils
{
    /// <summary>
    /// Base class of spawning things
    /// </summary>
    public class Spawner : Thing
    {
        public Thing _spawnObject;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _animationObject;
        public List<GameObject> SpawnList;


        /// <summary>
        /// Spawns thing
        /// </summary>
        public virtual Thing Spawn()
        {

            

            for (int i = 0; i < SpawnList.Count; i++)
            {
                if (!SpawnList[i].CompareTag("StartingPos"))
                {
                    SpawnList.RemoveAt(i);
                }
            }

            Thing instance = Instantiate((Thing)_spawnObject.Clone());

            if(SpawnList.Count==0)
                instance.transform.position = _spawnPoint != null ? _spawnPoint.position : transform.position;
            else
                instance.transform.position = _spawnPoint != null ? _spawnPoint.position : SpawnList[Random.Range(0, SpawnList.Count)].transform.position;

            return instance;
        }

        /// <summary>
        /// Spawns thing with animation and delay 
        /// </summary>
        /// <param name="animationTime">Delay (in ms)</param>
        public virtual async void Spawn(int animationTime)
        {

            _animationObject.SetActive(true);

            await Task.Delay(animationTime);

            _animationObject.SetActive(false);

            Spawn();
        }
    }
}
