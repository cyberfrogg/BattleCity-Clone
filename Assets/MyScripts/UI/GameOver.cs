using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject button;
    public void BackToMainMenu(InputAction.CallbackContext context)
    {
        button.GetComponent<Button>().onClick?.Invoke();
    }
}
