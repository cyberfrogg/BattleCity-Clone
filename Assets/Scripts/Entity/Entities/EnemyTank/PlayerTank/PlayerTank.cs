using GameUtils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities
{
    public class PlayerTank : Tank
    {
        [SerializeField] private PlayerTankAI _playerTankAI;
        [SerializeField] private ArrowPlayerTankController _arrowPlayerTankController;
        private Vector2 _movementVector;
        public AudioClip PlayerMovement;
        public AudioClip PlayerIdleAudio;
        public AudioClip PlayerDyingAudio;


        public override void Awake()
        {
            base.Awake();

            
            AI = _playerTankAI;

            AI.Init(this);
        }

        public override void Update()
        {
            base.Update();
            
            
        }

        public override void Damage(int damageCount, Entity damageOwner)
        {
            if(!_powerUps.PickedHelmet)
                base.Damage(damageCount, damageOwner);
        }

        public override void Die(Entity deathOwner)
        {
            AudioManager.Instance.PlaySFX(PlayerDyingAudio);

            Game.Instance.Triggers.OnPlayerKilled.Invoke();

            base.Die(deathOwner);
        }

        public override void Move(Vector2 moveVector)
        {

            if (moveVector.x != 0 && moveVector.y != 0)
                return;
            base.Move(moveVector);
        }

        public void MovePlayer(InputAction.CallbackContext context)
        {

            if (context.canceled)
            {
                AudioManager.Instance.PlayBackGroundSFX(PlayerIdleAudio);
            }

            if (context.started)
            {
                AudioManager.Instance.PlayBackGroundSFX(PlayerMovement);
            }
            Debug.Log("called player movement");
            Move(context.ReadValue<Vector2>());
        }

        public void ShootFromPlayer(InputAction.CallbackContext context)
        {
            Gun.Shoot();
        }
    }
}
