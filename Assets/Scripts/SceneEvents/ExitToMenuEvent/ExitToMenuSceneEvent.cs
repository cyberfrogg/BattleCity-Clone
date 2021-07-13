using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneEvents
{
    public class ExitToMenuSceneEvent : SceneEvent
    {
        [SerializeField] private string _menuSceneName;

        public override void TriggerEvent()
        {
            base.TriggerEvent();

            SceneManager.LoadScene(_menuSceneName);
        }
    }
}
