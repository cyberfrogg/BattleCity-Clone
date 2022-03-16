using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    public Sprite[] sprite;
    private int _spriteToShow;
    private Animator _animComp;
    private float duration;
    public SpriteRenderer sr;
    void Start()
    {
        _animComp = GetComponent<Animator>();
        duration = _animComp.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log(duration + " animation duration");
        ChangeSprite();
    }

    void Update()
    {

    }
    public void ShowTankScore()
    {

        Debug.Log("Show tank Score "+_spriteToShow);

    }

    public void SetTankScore(EnemyTankType tankType)
    {
        _spriteToShow = (int) tankType;
    }

    public async void ChangeSprite()
    {

        Debug.Log("Sprite Changed Started");
        await UniTask.Delay(TimeSpan.FromSeconds(duration));

        sr.sprite = sprite[_spriteToShow];

        Debug.Log("Sprite Chnaged again");
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        sr.sprite = null;
        Destroy(gameObject);

    }
}
