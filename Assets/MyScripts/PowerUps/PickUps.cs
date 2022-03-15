using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUps : MonoBehaviour
{
    public AudioClip PowerUpPickUpAudio;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(PowerUpPickUpAudio);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            PowerUp(other);
        }
    }

    protected abstract void PowerUp(Collider2D player);

}
