using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

public class LoadHighScore : MonoBehaviour
{
    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI LoadingScore;
    private bool _loadingData = true;

    public void SetHighScore()
    {
        _loadingData = false;
        LoadingScore.gameObject.SetActive(false);
        HighScore.text = GetHighScore().ToString("000000"); ;
    }

    private int GetHighScore()
    {
        return PlayFabController.Instance.TotalHighScore;
    }

    public async void SeeLoadingScreen()
    {
        int maxLoopCount = 9;
        int currentLoopCount = 1;
        while (_loadingData)
        {
            string dot = "";
            for (int i = 0; i < currentLoopCount; i++)
            {
                dot += ".";
            }

            currentLoopCount++;


            if (currentLoopCount> maxLoopCount)
            {
                currentLoopCount = 1;
            }


            LoadingScore.text = dot;

            await UniTask.Delay(TimeSpan.FromSeconds(.5f));

        }
    }
}
