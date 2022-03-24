using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollsion : MonoBehaviour
{

    public BoxCollider2D Tank;
    public BoxCollider2D TankCollisionBlocker;

    void Start()
    {
        Physics2D.IgnoreCollision(Tank,TankCollisionBlocker,true);
    }

}
