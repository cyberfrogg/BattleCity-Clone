using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities;
using GameUtils;
using UnityEngine;

public class Helmet : PickUps
{
    [SerializeField] private float _helmetDuration;
    private Collider2D _playerReference;
    private GameObject _helmet;
    public Sprite[] Sprite;
    protected override void PowerUp(Collider2D player)
    {
        _helmet = GameObject.FindGameObjectWithTag("Helmet");
        _playerReference = player;
        GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().PickedHelmet = true;
        _helmet.GetComponent<SpriteRenderer>().enabled = true;
        //_helmet.GetComponent<BoxCollider2D>().enabled = true;


        RemovePowerUp();
    }

    private async void RemovePowerUp()
    {

        int loopCount = Mathf.FloorToInt(_helmetDuration / .2f);

        for (int i = 0; i < loopCount; i++)
        {
            while (Game.Instance.IsGamePaused)
            {
                await UniTask.Yield();

            }

            _helmet.GetComponent<SpriteRenderer>().sprite = Sprite[i%Sprite.Length];
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
        }

       

        _helmet.GetComponent<SpriteRenderer>().enabled = false;
        //_helmet.GetComponent<BoxCollider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().PickedHelmet = false;
        Destroy(gameObject);
    }

}
