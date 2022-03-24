using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonSelectorHorizontal : MonoBehaviour
{
    public GameObject[] ButtonGameObjects;
    public Image[] ButtonIcon;
    private int _activatedButton = 0;

    public void SelectOption(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x < 0)
        {
            MoveLeft();
        }
        else if (context.ReadValue<Vector2>().x > 0)
        {
            MoveRight();
        }
    }


    public void SubmitOption(InputAction.CallbackContext context)
    {
        ButtonGameObjects[_activatedButton].GetComponent<Button>().onClick?.Invoke();
    }


    public void MoveLeft()
    {
        ButtonIcon[1].GetComponent<Image>().enabled = false;
        ButtonIcon[0].GetComponent<Image>().enabled = true;
        _activatedButton = 0;
    }

    public void MoveRight()
    {

        ButtonIcon[1].GetComponent<Image>().enabled = true;
        ButtonIcon[0].GetComponent<Image>().enabled = false;
        _activatedButton = 1;
    }
}
