using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDisabler : MonoBehaviour
{
    private SpriteRenderer[] sprite;

    void Awake()
    {
        sprite = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in sprite)
        {
            sr.enabled = false;
        }
    }
}
