using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabStats : MonoBehaviour
{
    public static PlayFabStats Instance;




    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

}
