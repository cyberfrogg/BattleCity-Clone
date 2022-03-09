using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class ArrowPlayerTankController : EntityAI
    {
        public override void UpdateAI()
        {
            base.UpdateAI();

            //(Self as Tank).Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

            (Self as Tank).Move(new Vector2(Input.GetAxis("ArrowHorizontalMovement"), Input.GetAxis("ArrowVerticalMovement")));

            if (Input.GetButtonDown("Fire2"))
            {
                (Self as Tank).Gun.Shoot();
            }
        }
    }
}