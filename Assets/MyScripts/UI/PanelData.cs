using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelData : MonoBehaviour
{
    public bool ShowLoginPanel;


    public static PanelData Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }


        
    }

    
}
