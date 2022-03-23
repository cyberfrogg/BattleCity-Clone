using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject button;
    public AudioClip ButtonClick;
    private bool _alreadyClicked;
    public void BackToMainMenu(InputAction.CallbackContext context)
    {
        if(!gameObject.activeInHierarchy)
            return;

        if (_alreadyClicked)
            return;

        if (context.performed)
        {
            PlayButtonClick();
        }
        
    }

    public async void PlayButtonClick()
    {
        
        if(_alreadyClicked)
            return;


        AudioManager.Instance.PlaySFX(ButtonClick);
        await UniTask.Delay(TimeSpan.FromSeconds(.3f));

        button.GetComponent<Button>().onClick?.Invoke();
        _alreadyClicked = true;
        button.GetComponent<Button>().interactable = false;
    }


}
