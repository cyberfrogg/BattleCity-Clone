using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScore : MonoBehaviour
{
    public static SaveScore Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayerPrefs.DeleteKey("TotalScore");
            PlayerPrefs.DeleteKey("PlayerLifeCount");
            PlayerPrefs.DeleteKey("StageCount");
            PlayerPrefs.DeleteKey("GunTier");
        }
        else
        {
            Destroy(gameObject);
        }


    }



}
