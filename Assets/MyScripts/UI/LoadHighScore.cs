using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using TMPro;

public class LoadHighScore : MonoBehaviour
{
    public TextMeshProUGUI HighScore;

    public void SetHighScore()
    {
        HighScore.text = GetHighScore().ToString("000000"); ;
    }

    private int GetHighScore()
    {
        return PlayFabController.Instance.TotalHighScore;
    }
}
