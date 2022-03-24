using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneEvents
{
    public class MainMenuLoad : SceneEvent
    {
        //[SerializeField] private string _leaderBoardScene;
 

        public override void TriggerEvent()
        {
            base.TriggerEvent();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }
}
