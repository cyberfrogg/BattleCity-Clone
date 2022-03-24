using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            await Task.Delay(2000);
            SceneManager.LoadScene("menu");
        }
    }
}
