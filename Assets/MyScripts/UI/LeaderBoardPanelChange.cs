using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoardPanelChange : MonoBehaviour
{

    public TextMeshProUGUI ChangeRankingText;


    public void ChangeText(string text)
    {
        ChangeRankingText.text = text;
    }
}
