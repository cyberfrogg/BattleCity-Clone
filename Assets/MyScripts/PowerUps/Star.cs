using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Guns;
using UnityEngine;

public class Star : PickUps
{
    private Collider2D _playerReference;

    protected override void PowerUp(Collider2D player)
    {
        _playerReference = player;
        GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().Star = true;
        _playerReference.GetComponent<PlayerGun>().ChangeTier();

    }

}
