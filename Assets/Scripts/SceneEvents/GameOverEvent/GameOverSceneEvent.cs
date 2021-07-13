using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SceneEvents
{
    public class GameOverSceneEvent : SceneEvent
    {
        [SerializeField] private GameObject _gameoverPopupShow;
        [SerializeField] private GameObject _gamveoverStatsWindow;

        public override void TriggerEvent()
        {
            base.TriggerEvent();

            _gameoverPopupShow.SetActive(true);

            showStatsWindow();
        }

        private async void showStatsWindow()
        {
            await Task.Delay(2000);

            _gamveoverStatsWindow.SetActive(true);
        }
    }
}
