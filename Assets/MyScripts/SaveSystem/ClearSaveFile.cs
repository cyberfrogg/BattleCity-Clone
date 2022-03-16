using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSaveFile : MonoBehaviour
{
    
    public void ClearSave()
    {
        PlayerPrefs.DeleteKey("TotalScore");
        PlayerPrefs.DeleteKey("PlayerLifeCount");
        PlayerPrefs.DeleteKey("StageCount");
        PlayerPrefs.DeleteKey("GunTier");
    }
}
