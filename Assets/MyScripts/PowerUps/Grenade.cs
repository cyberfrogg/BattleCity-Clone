using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameUtils;
using UnityEngine;

public class Grenade : PickUps
{
    

    protected override void PowerUp(Collider2D player)
    {
        GameObject.FindGameObjectWithTag("Game").GetComponent<Waves>().DestroyWave();
        Destroy(gameObject);
    }

}
