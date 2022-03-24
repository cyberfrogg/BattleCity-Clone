using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonSelectorSignUp : MonoBehaviour
{
    public GameObject ButtonGameObjects;


    public void SubmitOption(InputAction.CallbackContext context)
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (context.performed)
            InvokeMethod();
    }


    public async void InvokeMethod()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        ButtonGameObjects.GetComponent<Button>().onClick?.Invoke();
    }
}
