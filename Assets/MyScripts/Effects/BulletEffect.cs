using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class BulletEffect : MonoBehaviour
{

    //public BulletAnimation _bulletEffect;
    void Awake()
    {

        //_bulletEffect.AddListener(PlayEffect);
    }

    void Start()
    {
        PlayEffect();
    }
    private async void PlayEffect()
    {

        int childCount = transform.childCount;

        
        transform.GetChild(--childCount).GetComponent<SpriteRenderer>().enabled = true;
        //await UniTask.Delay(TimeSpan.FromSeconds(1f));

        await UniTask.DelayFrame(5);
        transform.GetChild(childCount).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(--childCount).GetComponent<SpriteRenderer>().enabled = true;
        await UniTask.DelayFrame(5);
        //await UniTask.Delay(TimeSpan.FromSeconds(1f));

        transform.GetChild(childCount).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(--childCount).GetComponent<SpriteRenderer>().enabled = true;
        await UniTask.DelayFrame(5);
        //await UniTask.Delay(TimeSpan.FromSeconds(1f));

        transform.GetChild(childCount).GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);

    }
}

[System.Serializable]
public class BulletAnimation : UnityEvent<Vector3>
{
}
