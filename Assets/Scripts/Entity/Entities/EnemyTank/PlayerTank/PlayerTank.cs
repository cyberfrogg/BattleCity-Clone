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
        private Vector2 movePosition;

        public override void Awake()
        {
            base.Awake();


            AI = _playerTankAI;

            AI.Init(this);
        }

        public override void Update()
        {
            base.Update();
            transform.position += new Vector3(movePosition.x, movePosition.y, 0f) * MoveSpeed * Time.deltaTime;
            //transform.Translate(movePosition * MoveSpeed * Time.deltaTime);
        }

        void FixedUpdate()
        {
            //_rigidbody.velocity = (movePosition * MoveSpeed);
            //_rigidbody.MovePosition(_rigidbody.position + (new Vector2(movePosition.x , movePosition.y) * MoveSpeed * Time.deltaTime));
            //_rigidbody.AddForce(movePosition * MoveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
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
            
            //if (moveVector.x != 0 && moveVector.y != 0)
            //    return;
            //base.Move(moveVector);

            if (moveVector.x != 0 && moveVector.y != 0)
            {
                movePosition = Vector2.zero;
                return;
            }
                


            if (moveVector.x != 0 || moveVector.y != 0)
            {
                LookAt(moveVector);
            }

            movePosition = moveVector;

            //transform.position += new Vector3(moveVector.x, moveVector.y, 0f) * MoveSpeed;

        }

        public void MovePlayer(InputAction.CallbackContext context)
        {
            if (Game.Instance.isLevelDone)
            {
                GetComponent<PlayerInput>().enabled = false;
                AudioManager.Instance.StopAll();
                return;
            }


            if (context.canceled)
            {
                AudioManager.Instance.PlayBackGroundSFX(PlayerIdleAudio);
            }

            if (context.started)
            {
                AudioManager.Instance.PlayBackGroundSFX(PlayerMovement);
            }
            Debug.Log("called from player movement");
            Move(context.ReadValue<Vector2>());

            Debug.Log("Called from player input "+ context.ReadValue<Vector2>());
        }

        public void ShootFromPlayer(InputAction.CallbackContext context)
        {
            if (Game.Instance.isLevelDone)
            {
                AudioManager.Instance.StopAll();
                return;
            }
            Gun.Shoot();
        }

        public override void OnThingCollidedEnter(Thing thing)
        {
            base.OnThingCollidedEnter(thing);
            //movePosition = Vector2.zero;
        }

    }
}
