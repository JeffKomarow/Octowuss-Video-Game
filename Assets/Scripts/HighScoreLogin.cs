using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;

public class HighScoreLogin : MonoBehaviour
{

    public InputField userNameField;
    public InputField userPassField;
    public Button registerButton;
    public PlayFabManager PlayFabManager;

    private string _userName = null;
    private string _userPass = null;

    public void SetUserName()
    {
        _userName = userNameField.text;
    }
    public void SetUserPass()
    {   
        _userPass = userPassField.text;
    }
    public string GetUserName()
    {
        return _userName;
    }
    public string GetUserPass()
    {
        return _userPass;
    }

    public void DisplayInfo()
    {
        Debug.Log("Name: " + GetUserName());
        Debug.Log("Pass: " + GetUserPass());
    }

    public void LoginWithPassword()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = _userName,
            Password = _userPass,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true,
                GetUserAccountInfo = true,
                GetUserData = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in as: " + result.InfoResultPayload.PlayerProfile.DisplayName);
        Debug.Log("COUNT" + result.InfoResultPayload.AccountInfo.CustomIdInfo.CustomId);
        PlayFabManager.OnPasswordLoginSuccess(result.InfoResultPayload.AccountInfo.CustomIdInfo.CustomId, result.InfoResultPayload.PlayerProfile.DisplayName);
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void PasswordChanged()
    {
        if (userPassField.text.Length >= 6)
            registerButton.interactable = true;
        else
            registerButton.interactable = false;

        //registerButton.interactable = userPassField.text.Length >= 6;
    }

}
