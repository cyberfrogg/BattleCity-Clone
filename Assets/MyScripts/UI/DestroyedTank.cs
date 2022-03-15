using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public enum EnemyTankType
{
    Basic,
    Fast,
    Power,
    Armor
}

public class DestroyedTank : MonoBehaviour
{
    public static DestroyedTank instance;

    
    public Dictionary<EnemyTankType, int> TankTypeDestroyed = new Dictionary<EnemyTankType, int>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        TankTypeDestroyed.Add(EnemyTankType.Basic, 0);
        TankTypeDestroyed.Add(EnemyTankType.Fast, 0);
        TankTypeDestroyed.Add(EnemyTankType.Power, 0);
        TankTypeDestroyed.Add(EnemyTankType.Armor, 0);
    }

}


