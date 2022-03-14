using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSelector : MonoBehaviour
{
    public void SelectGameMode(int mode)
    {
        PlayerPrefs.SetInt("GameMode",mode);
    }
}
