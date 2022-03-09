﻿using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class PlayerTankAI : EntityAI
    {
        public override void UpdateAI()
        {
            base.UpdateAI();

            //(Self as Tank).Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

            (Self as Tank).Move(new Vector2(Input.GetAxis("KeyBoardHorizontalMovement"), Input.GetAxis("KeyBoardVerticalMovement")));

            if (Input.GetButtonDown("Fire1"))
            {
                (Self as Tank).Gun.Shoot();
            }
        }
    }
}
