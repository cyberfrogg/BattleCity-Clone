using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonSelectorForLeaderBoard : MonoBehaviour
{
    public GameObject[] ButtonGameObjects;
    private int _activatedButton = 0;

    public AudioClip ButtonClickSound;
    public AudioClip ButtonSelectionSound;

    public void SelectOption(InputAction.CallbackContext context)
    {

        if (!gameObject.activeInHierarchy)
            return;

        Debug.Log(("Naviagtion Button CLicked"));

        if (context.performed)
        {

            AudioManager.Instance.PlaySFX(ButtonSelectionSound);

            if (context.ReadValue<Vector2>().x < 0)
            {

                MoveLeft();
            }
            else if (context.ReadValue<Vector2>().x > 0)
            {
                MoveRight();
            }
        }

        
    }


    public void SubmitOption(InputAction.CallbackContext context)
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (context.performed)
            InvokeMethod();


    }


    public void MoveLeft()
    {

        _activatedButton--;

        if (_activatedButton < 0)
            _activatedButton = 0;

        foreach (var buttton in ButtonGameObjects)
        {
            buttton.GetComponent<Image>().enabled = false;
        }

        ButtonGameObjects[_activatedButton].GetComponent<Image>().enabled = true;

    }

    public void MoveRight()
    {


        _activatedButton++;

        if (_activatedButton >= ButtonGameObjects.Length)
            _activatedButton = ButtonGameObjects.Length - 1;

        foreach (var buttton in ButtonGameObjects)
        {
            buttton.GetComponent<Image>().enabled = false;
        }

        ButtonGameObjects[_activatedButton].GetComponent<Image>().enabled = true;
    }

    public async void InvokeMethod()
    {

        AudioManager.Instance.PlaySFX(ButtonClickSound);
        await UniTask.Delay(TimeSpan.FromSeconds(.5f));

        ButtonGameObjects[_activatedButton].GetComponent<Button>().onClick?.Invoke();
    }
}
