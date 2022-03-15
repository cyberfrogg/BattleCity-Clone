using System;
using Cysharp.Threading.Tasks;
using Entities;
using UnityEngine;
using GameUtils;
using Statistics;

namespace Blocks
{
    public class CityBlock : Block
    {
        [SerializeField] private Sprite _deadSprite;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private int _maxHealth = 1;
        private int _currentHealth;
        private bool _isDied;
        public GameObject[] BrickWalls;
        public GameObject[] SteelWalls;
        private PlayerPowerUps _powerUps;
        private bool _previousState;
        private bool _currentState;
        public AudioClip DestroyedSfx;
        private LevelStatisticsCollector _levelStatisticsCollector;

        public override void Start()
        {
            _currentState = _previousState;
            base.Start();
            _levelStatisticsCollector =
                GameObject.FindGameObjectWithTag("Game").GetComponent<LevelStatisticsCollector>();
            _currentHealth = _maxHealth;
            _powerUps = GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>();

        }

        public override void Update()
        {

            _currentState = _powerUps.PickedShovel;
            
            if (_currentState != _previousState)
            {
                ChangeTile();
                Debug.Log("Called this function");
            }

        }



        public override void OnThingCollidedEnter(Thing thing)
        {
            base.OnThingCollidedEnter(thing);

            if(thing is Bullet)
            {
                Bullet bullet = thing as Bullet;
                Damage(bullet.DamageCount);
            }
        }

        public void Damage(int damage)
        {
            _currentHealth -= damage;
            validateHealth();
        }
        private void validateHealth()
        {
            if (_currentHealth <= 0)
            {
                if (!_isDied)
                {
                    AudioManager.Instance.PlaySFX(DestroyedSfx);
                    _levelStatisticsCollector.SetHighScore();
                    _isDied = true;
                    Game.Instance.Triggers.OnCityBlockDied.Invoke();
                    _spriteRenderer.sprite = _deadSprite;
                }
            }
        }

        public void ChangeTile()
        {

            _previousState = _currentState;

            if (_currentState)
            {
                foreach (var wall in SteelWalls)
                {
                    wall.SetActive(true);   
                }

                foreach (var wall in BrickWalls)
                {
                    wall.SetActive(false);
                }
            }
            else
            {
                BlinkingEffect();
            }
        }


        private async void BlinkingEffect()
        {
            bool state = true;
            for (int i = 0; i < 15; i++)
            {
                state = !state;
                if (!state)
                {

                    foreach (var wall in BrickWalls)
                    {
                        wall.GetComponent<ChildCount>().EnableChildren();
                    }

                    foreach (var wall in SteelWalls)
                    {
                        wall.SetActive(false);
                    }
                }
                else
                {
                    foreach (var wall in SteelWalls)
                    {
                        wall.SetActive(true);
                    }

                    foreach (var wall in BrickWalls)
                    {
                        wall.SetActive(false);
                    }
                }

                await UniTask.Delay(TimeSpan.FromSeconds(.1f));


            }





        }
    }
}