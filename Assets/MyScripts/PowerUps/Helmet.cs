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
    public Sprite[] Sprite;
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

        int loopCount = Mathf.FloorToInt(_helmetDuration / .5f);

        for (int i = 0; i < loopCount; i++)
        {
            _helmet.GetComponent<SpriteRenderer>().sprite = Sprite[i%Sprite.Length];
            await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        }

       

        _helmet.GetComponent<SpriteRenderer>().enabled = false;
        _helmet.GetComponent<BoxCollider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().PickedHelmet = false;
        Destroy(gameObject);
    }

}
