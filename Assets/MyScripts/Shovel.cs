using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Shovel : PickUps
{
    [SerializeField] private float _timerDuration;
    private Collider2D _playerReference;

    protected override void PowerUp(Collider2D player)
    {
        _playerReference = player;
        GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().PickedShovel = true;

        
        //Do fade
        RemovePowerUp();
    }

    private async void RemovePowerUp()
    {

        await UniTask.Delay(TimeSpan.FromSeconds(_timerDuration));

        GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().PickedShovel = false;
        Destroy(gameObject);
    }
}
