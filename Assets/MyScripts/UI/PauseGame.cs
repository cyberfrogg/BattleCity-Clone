using System.Collections;
using System.Collections.Generic;
using GameUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{

    public GameObject Pause;
    private bool _state;

    public void EnableDisablePause(InputAction.CallbackContext context)
    {
        

        if (context.performed)
        {
            Debug.Log("Pause button clicked");
            Game.Instance.ChangeState();
        }

    }

    public void PauseGamePanel()
    {
        Pause.SetActive(!Pause.activeInHierarchy);
    }
}
