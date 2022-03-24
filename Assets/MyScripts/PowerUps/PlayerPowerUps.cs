using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    public bool PickedHelmet;
    public bool PickedGrenade;
    public bool PickedShovel;
    public bool Star;
    public bool Tank;
    public bool Timer;
    public bool LevelCleared;

    private GameObject _helmet;


    void Start()
    {
        _helmet = GameObject.FindWithTag("Helmet");
    }

    void Update()
    {

    }
}
