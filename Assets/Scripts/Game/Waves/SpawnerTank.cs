using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTank
{
    public int Spawner(int tierNo)
    {
        List<int> tankList = new List<int>();
        for (int i = 0; i <= tierNo; i++)
        {
            tankList.Add(i < tierNo ? i : tierNo);
        }
        return tankList[Random.Range(0, tankList.Count)];
    }
}
