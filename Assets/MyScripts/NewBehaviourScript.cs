using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private WebCamDevice[] devices;
    private static WebCamTexture webcam;
    // Start is called before the first frame update
    void Start()
    {
        webcam = new WebCamTexture();

        devices = WebCamTexture.devices;

        foreach (var device in devices)
        {
            Debug.Log(device.name);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}



