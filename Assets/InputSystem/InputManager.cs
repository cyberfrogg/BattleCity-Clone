using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public void RemoveInputFromPlayer()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>());

    }
}
