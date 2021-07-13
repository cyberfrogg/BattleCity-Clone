using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
