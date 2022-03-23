using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace SceneEvents
{
    public class ExitToMenuSceneEvent : SceneEvent
    {
        [SerializeField] private string _menuSceneName;

        public override void TriggerEvent()
        {
            base.TriggerEvent();

            LoadMenuScene();
        }

        private async void LoadMenuScene()
        {
            await Task.Delay(2000);
            SceneManager.LoadScene(_menuSceneName);

        }
    }
}
