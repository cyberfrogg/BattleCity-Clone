using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActivision : MonoBehaviour
{

    public GameObject LoginPanel;
    public GameObject StartingPanel;

    void Start()
    {

        

        if (PanelData.Instance.ShowLoginPanel)
            LoginPanel.SetActive(true);
        else
            StartingPanel.SetActive(true);

        PanelData.Instance.ShowLoginPanel = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
