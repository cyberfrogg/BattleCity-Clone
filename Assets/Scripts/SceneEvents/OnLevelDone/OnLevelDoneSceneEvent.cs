using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneEvents
{
    public class OnLevelDoneSceneEvent : SceneEvent
    {
        [SerializeField] private string _nextLevelScene;
        [SerializeField] private int _nextLevelDelay = 5000;
        [SerializeField] private GameObject _levelOverScreen;


        public override void TriggerEvent()
        {
            base.TriggerEvent();

            _levelOverScreen.gameObject.SetActive(true);

            LoadNextLevel();
        }

        private async void LoadNextLevel()
        {
            await Task.Delay(_nextLevelDelay);
            SceneManager.LoadScene(_nextLevelScene);
        }
    }
}
