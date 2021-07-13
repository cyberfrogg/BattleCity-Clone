using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneEvents
{
    public class MenuExitGameSceneEvent : SceneEvent
    {
        public override void TriggerEvent()
        {
            base.TriggerEvent();

            Debug.Log("Exiting game");
            Application.Quit();
        }
    }
}
