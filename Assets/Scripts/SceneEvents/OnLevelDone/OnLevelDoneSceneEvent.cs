using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameUtils;
using Guns;
using Statistics;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;


namespace SceneEvents
{
    public class OnLevelDoneSceneEvent : SceneEvent
    {

        [SerializeField] private GameObject _levelOverScreen;


        public override void TriggerEvent()
        {
            base.TriggerEvent();

            _levelOverScreen.gameObject.SetActive(true);
            AudioManager.Instance.StopAll();

            _levelOverScreen.GetComponent<LevelEndUI>().Initialization();
        }




    }
}
