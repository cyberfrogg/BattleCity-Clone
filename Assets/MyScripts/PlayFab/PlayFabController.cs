using System.Collections.Generic;
using System.Linq.Expressions;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.PfEditor.Json;
using UnityEngine;
using JsonObject = PlayFab.Json.JsonObject;


public class PlayFabController : MonoBehaviour
{
    private string _userEmail;
    private string _userPassword;
    private string _userName;


    public GameObject LoginPanel;
    public GameObject StartingPanel;

    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "A66CC";
        }

        if (PlayerPrefs.HasKey("LoginEmail"))
        {

            SetUserEmail(PlayerPrefs.GetString("LoginEmail"));
            SetUserPassWord(PlayerPrefs.GetString("LoginPassword"));

            var request = new LoginWithEmailAddressRequest { Email = _userEmail, Password = _userPassword };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }


    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        LoginPanel.SetActive(false);
        StartingPanel.SetActive(true);
        PlayerPrefs.SetString("LoginEmail", _userEmail);
        PlayerPrefs.SetString("LoginPassword", _userPassword);
        PlayerPrefs.SetString("LoginName", _userName);

        GetStats();
    }

    private void OnLoginFailure(PlayFabError error)
    {

        Debug.LogError(error.GenerateErrorReport());


        var registerNewUser = new RegisterPlayFabUserRequest { Email = _userEmail , Password = _userPassword , Username = _userName};
        PlayFabClientAPI.RegisterPlayFabUser(registerNewUser, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");

        PlayerPrefs.SetString("LoginEmail" , _userEmail);
        PlayerPrefs.SetString("LoginPassword", _userPassword);
        PlayerPrefs.SetString("LoginName", _userName);
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest {DisplayName = _userName},
            OnDisplayName, OnLoginFailure);

        LoginPanel.SetActive(false);
        StartingPanel.SetActive(true);
        GetStats();

    }

    void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + " is your new display name");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void SetUserEmail(string email)
    {
        _userEmail = email;
    }

    public void SetUserPassWord(string password)
    {
        _userPassword = password;
    }

    public void SetUserName(string uName)
    {
        _userName = uName;
    }

    private string GetUserEmail()
    {
        return _userEmail;
    }

    private string GetUserPassword()
    {
        return _userPassword;
    }

    private string GetUserName()
    {
        return _userName;
    }

    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = _userEmail, Password = _userPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }







    public int PlayerCompletedLevel;
    public int PlayerHighScore;
    public int PlayerId;

    public void SetStats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate> {
                    new StatisticUpdate { StatisticName = "PlayerCompletedLevel", Value = PlayerCompletedLevel },
                    new StatisticUpdate { StatisticName = "PlayerHighScore", Value = PlayerHighScore },
                    new StatisticUpdate { StatisticName = "PlayerId", Value = PlayerId },
                }
        },
            result => { Debug.Log("User statistics updated"); },
            error => { Debug.LogError(error.GenerateErrorReport()); });
    }

    void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);

            switch (eachStat.StatisticName)
            {
                case "PlayerCompletedLevel":
                    PlayerCompletedLevel = eachStat.Value;
                    break;

                case "PlayerHighScore":
                    PlayerHighScore = eachStat.Value;
                    break;

                case "PlayerId":
                    PlayerId = eachStat.Value;
                    break;
            }
        }
            
    }


    public void StartCloudUpdatePlayerStats()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats",
            FunctionParameter = new { _playerCompletedLevel = PlayerCompletedLevel , _playerHighScore = PlayerHighScore, _playerId = PlayerId },
            GeneratePlayStreamEvent = true, 
        }, OnCloudUpdateStats, OnErrorShared);
    }

    private static void OnCloudUpdateStats(ExecuteCloudScriptResult result)
    {
        //Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue);
        Debug.Log((string)messageValue);
    }

    private static void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }




    public GameObject LeaderBoardPanel;
    public GameObject LeaderBoardRowPrefab;
    public GameObject LeaderBoardContainer;


    public void GetLeaderBoard()
    {
        var requestLeaderBoard = new GetLeaderboardRequest {StartPosition = 0, StatisticName = "PlayerHighScore" , MaxResultsCount = 10};
        PlayFabClientAPI.GetLeaderboard(requestLeaderBoard,onGetLeaderBoard, OnErrorLeaderBoard );
    }

    void onGetLeaderBoard(GetLeaderboardResult result)
    {
        StartingPanel.SetActive(false);
        LeaderBoardPanel.SetActive(true);
        int i = 1;

        Debug.Log("Called LeaderBoard");

        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {

            GameObject instantiateRow = Instantiate(LeaderBoardRowPrefab, LeaderBoardContainer.transform);

            LeaderBoard ld = instantiateRow.GetComponent<LeaderBoard>();

            ld.PlayerRank.text = i.ToString("000");
            ld.PlayerName.text = player.DisplayName;
            ld.PlayerScore.text = player.StatValue.ToString("00000");

            Debug.Log(player.DisplayName + ": " + player.StatValue);

            i++;
        }
    }

    void OnErrorLeaderBoard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void CloseLeaderBoard()
    {
        LeaderBoardPanel.SetActive(false);

        StartingPanel.SetActive(true);

        Debug.Log("Calling LeaderBoard Close");

        for (int i = LeaderBoardContainer.transform.childCount - 1; i >=0; i--)
        {
            Destroy(LeaderBoardContainer.transform.GetChild(i).gameObject);
        }

        

    }
}