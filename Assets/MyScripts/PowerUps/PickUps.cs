using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class PickUps : MonoBehaviour
{
    public AudioClip PowerUpPickUpAudio;
    private float PowerUpPickUpTime = 10f;
    private bool _picked;
    [SerializeField] private bool _noBlinkingEffect;

    void Start(){
        if(!_noBlinkingEffect)
            SpriteBlinking();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _picked = true;
            AudioManager.Instance.PlaySFX(PowerUpPickUpAudio);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            PowerUp(other);
        }
    }

    protected async void SpriteBlinking()
    {
        bool spriteState = false;
        int loopCount = (int)(PowerUpPickUpTime / .2f);
        for (int i = 0; i < loopCount; i++)
        {
            if(_picked) return;
            GetComponent<SpriteRenderer>().enabled = spriteState;
            spriteState = !spriteState;
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
        }

        Destroy(gameObject);
    }
    

    protected abstract void PowerUp(Collider2D player);

}
