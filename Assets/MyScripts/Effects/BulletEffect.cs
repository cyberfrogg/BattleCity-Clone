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
        int currentChildCount = childCount--;

        while (currentChildCount>=-1 && !GameUtils.Game.Instance.IsGamePaused)
        {
            if ((0 <= (currentChildCount+1)) && ((currentChildCount+1) < childCount))
            {
                transform.GetChild(currentChildCount + 1).GetComponent<SpriteRenderer>().enabled = false;
            }

            if (0 <= currentChildCount && currentChildCount < childCount)
            {
                transform.GetChild(currentChildCount).GetComponent<SpriteRenderer>().enabled = true;
            }

            

            await UniTask.DelayFrame(5);


            currentChildCount--;
        }




        /*transform.GetChild(--childCount).GetComponent<SpriteRenderer>().enabled = true;
        

        await UniTask.DelayFrame(5);
        transform.GetChild(childCount).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(--childCount).GetComponent<SpriteRenderer>().enabled = true;
        await UniTask.DelayFrame(5);
        

        transform.GetChild(childCount).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(--childCount).GetComponent<SpriteRenderer>().enabled = true;
        await UniTask.DelayFrame(5);
        

        transform.GetChild(childCount).GetComponent<SpriteRenderer>().enabled = false;*/
        Destroy(gameObject);

    }
}

[System.Serializable]
public class BulletAnimation : UnityEvent<Vector3>
{
}
