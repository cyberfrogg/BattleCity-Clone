using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneEvents
{
    public class MainMenuLeaderBoard : SceneEvent
    {
        [SerializeField] private string _leaderBoardScene;
 

        public override void TriggerEvent()
        {
            base.TriggerEvent();

            SceneManager.LoadScene(_leaderBoardScene);


        }
    }
}
