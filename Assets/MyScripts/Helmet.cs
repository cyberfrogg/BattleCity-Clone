using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities;
using UnityEngine;

public class Helmet : PickUps
{
    [SerializeField] private float _helmetDuration;
    private Collider2D _playerReference;
    private GameObject _helmet;
    protected override void PowerUp(Collider2D player)
    {
        _helmet = GameObject.FindGameObjectWithTag("Helmet");
        _playerReference = player;
        GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().PickedHelmet = true;
        _helmet.GetComponent<SpriteRenderer>().enabled = true;
        _helmet.GetComponent<BoxCollider2D>().enabled = true;


        //Do fade
        RemovePowerUp();
    }

    private async void RemovePowerUp()
    {
        
        await UniTask.Delay(TimeSpan.FromSeconds(_helmetDuration));

        _helmet.GetComponent<SpriteRenderer>().enabled = false;
        _helmet.GetComponent<BoxCollider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().PickedHelmet = false;
        Destroy(gameObject);
    }

}
