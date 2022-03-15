using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnPoint : MonoBehaviour
{
    public GameObject[] objects;

    public async void Initialize()
    {
        GameObject tile = (GameObject)Instantiate(objects[Random.Range(0, objects.Length)], transform.position, Quaternion.identity);
        tile.transform.parent = transform;

        var tilesSpawnPoints = tile.GetComponentsInChildren<SpawnPoint>().ToList();
        foreach (var x in tilesSpawnPoints)
        {
            x.Initialize();
            await UniTask.Delay(TimeSpan.FromSeconds(.1f), ignoreTimeScale: false);
        }

    }
}
