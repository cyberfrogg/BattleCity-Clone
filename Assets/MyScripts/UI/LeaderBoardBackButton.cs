using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LeaderBoardBackButton : MonoBehaviour
{
    public Button BackButton;
    public void SelectBackButton(InputAction.CallbackContext context)
    {
       BackButton.GetComponent<Button>().onClick?.Invoke();
    }


}
