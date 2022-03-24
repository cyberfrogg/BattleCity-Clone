using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameUtils;
using UnityEngine;

public class Timer : PickUps
{
    [SerializeField] private float _timerDuration;
    private Collider2D _playerReference;

    protected override void PowerUp(Collider2D player)
    {
        _playerReference = player;
        GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().Timer = true;


        //Do fade
        RemovePowerUp();
    }

    private async void RemovePowerUp()
    {


        int loopCount = Mathf.FloorToInt(_timerDuration / .2f);

        for (int i = 0; i < loopCount; i++)
        {
            while (Game.Instance.IsGamePaused)
            {
                await UniTask.Yield();

            }
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
        }


        GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().Timer = false;
        Destroy(gameObject);
    }
}
