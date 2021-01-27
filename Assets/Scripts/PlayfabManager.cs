using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField inputField;
    private string id;

    [Header("Register")]
    public InputField registerLogin;
    public InputField registerEmail;
    public InputField registerPassword;
    public GameObject joinTheHighScoresScreen;

    [Header("Leaderboard")]
    public Transform leaderboardRowsParent;
    public GameObject leaderboardRowPrefab;

    [Header("Misc")]
    public GameObject scoreSavedScreen;
    public GameObject accountCreatedScreen;
    public Text usernameText;

    [Header("Reset")]
    public InputField recoveryEmailInput;
    public GameObject passwordSent;

    private HighScoreLogin _highScoreLogin;
    bool sendScores = true;

    static string firstId = null;

    void Start()
    {
        FindObjectOfType<HighScoreLogin>();

        Login();
    }

    public void SendLeaderboard(int score)
    {
        if (sendScores == false)
        {
            Debug.Log("Score not sent - player not logged in!");
            return;
        }

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "OctowussScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "OctowussScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        Debug.Log("Leaderboard recieved!");

        foreach (Transform item in leaderboardRowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            Debug.Log("\nPos: " + item.Position + "\nScore: " + item.StatValue + "\nID: " + item.Profile.DisplayName);

            GameObject tempGo = Instantiate(leaderboardRowPrefab, leaderboardRowsParent);
            //tempGo.GetComponent<ScoreRow>().sampleText.text = "test";
            Text[] texts = tempGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.Profile.DisplayName;
            texts[2].text = item.StatValue.ToString();
        }


        
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent");
        scoreSavedScreen.SetActive(true);

    }
    void UpdateDisplayName(string nick)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nick
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, result => {
            Debug.Log("The player's display name is now: " + result.DisplayName);
        }, OnError);
    }

    public void Register()
    {
        var request = new AddUsernamePasswordRequest
        {
            Username = registerLogin.text,
            Password = registerPassword.text,
            Email = registerEmail.text,
        };
        PlayFabClientAPI.AddUsernamePassword(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(AddUsernamePasswordResult result)
    {
        Debug.Log("Registered!");
        UpdateDisplayName(registerLogin.text);
        joinTheHighScoresScreen.SetActive(false);
        usernameText.text = registerLogin.text;
        usernameText.gameObject.SetActive(true);
    }

    public void OnPasswordLoginSuccess(string newId, string displayName)
    {
        usernameText.text = displayName;
        usernameText.gameObject.SetActive(true);
        firstId = newId;
    }

    void Login()
    {
        Debug.Log("Starting to logging in...");
        string customId;
        if (firstId == null)
        {
            customId = SystemInfo.deviceUniqueIdentifier;
            #if UNITY_WEBGL
            customId = Random.Range(0, 100000000).ToString();
#endif
            firstId = customId;
            Debug.Log("First id is null!");
        } else
        {
            Debug.Log("Game restarted!");
            customId = firstId;
        }
        Debug.Log(customId);


        
        var request = new LoginWithCustomIDRequest
        {
            //CustomId = SystemInfo.deviceUniqueIdentifier, <- If Building for the desktop/mobile
            CustomId = customId, // <- WebGL
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetUserAccountInfo = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    void OnSuccess(LoginResult result) {
        string username = null;
        if (result.InfoResultPayload.AccountInfo.Username != null) {
            username = result.InfoResultPayload.AccountInfo.Username;
        }

        if (result.NewlyCreated == true || username == null)
        {
            Debug.Log("No username!");
            sendScores = false;
        } else if (username != null)
        {
            Debug.Log("Welcome " + username);
            usernameText.text = username;
            usernameText.gameObject.SetActive(true);
            // WELCOME SCREEN
        }
        Debug.Log("Successful Login/account create!");
        accountCreatedScreen.SetActive(true);
        
    }

    public void ResetPassword()
    {
        var request = new SendAccountRecoveryEmailRequest()
        {
            Email = recoveryEmailInput.text,
            TitleId = "CDD52"
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnResetPasswordSuccess, OnError);
    }

    void OnResetPasswordSuccess(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Recovery email sent!");
        passwordSent.SetActive(true);
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }
}