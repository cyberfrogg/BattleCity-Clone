using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadHighScore : MonoBehaviour
{
    public TextMeshProUGUI HighScore;

    void Awake()
    {
        HighScore.text = GetHighScore().ToString("000000"); ;
    }

    private int GetHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
            return PlayerPrefs.GetInt("HighScore");
        else
            return 0;
    }
}
