using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;

public class Life : PickUps
{
    protected override void PowerUp(Collider2D player)
    {
        GameObject.FindGameObjectWithTag("Game").GetComponent<LevelStatisticsCollector>().IncreaseLife();
    }
}
