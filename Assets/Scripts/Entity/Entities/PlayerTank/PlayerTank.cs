using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    /// <summary>
    /// Tank of Player
    /// </summary>
    public class PlayerTank : Tank
    {
        public override void Update()
        {
            base.Update();

            Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

            if (Input.GetButtonDown("Fire1"))
            {
                Gun.Shoot();
            }
        }
    }
}
