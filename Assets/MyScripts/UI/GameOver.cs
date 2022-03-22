using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject button;
    public AudioClip ButtonClick;
    public void BackToMainMenu(InputAction.CallbackContext context)
    {
        AudioManager.Instance.PlaySFX(ButtonClick);
        button.GetComponent<Button>().onClick?.Invoke();
    }
}
