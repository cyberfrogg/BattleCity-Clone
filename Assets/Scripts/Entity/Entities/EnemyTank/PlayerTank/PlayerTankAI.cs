using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities
{
    [Serializable]
    public class PlayerTankAI : EntityAI
    {
        private Vector2 _movementVector;
        public override void UpdateAI()
        {
            base.UpdateAI();

            //(Self as Tank).Move(_movementVector);

            //(Self as Tank).Move(new Vector2(Input.GetAxis("KeyBoardHorizontalMovement"), Input.GetAxis("KeyBoardVerticalMovement")));

            /*if (Input.GetButtonDown("Fire1"))
            {
                (Self as Tank).Gun.Shoot();
            }*/
        }

        /*public void MovePlayer(InputAction.CallbackContext context)
        {
            _movementVector = context.ReadValue<Vector2>();
            (Self as Tank).Move(_movementVector);
        }

        public void ShootFromPlayer(InputAction.CallbackContext context)
        {
            (Self as Tank).Gun.Shoot();
        }*/
    }
}
