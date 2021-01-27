using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject gameOverScreen;
    [SerializeField]
    public GameObject greatJobScreen;
    [SerializeField]
    public GameObject awesomeJobScreen;
    [SerializeField]
    public GameObject insaneJobScreen;
    [SerializeField]
    public GameObject aboutScreen;
    [SerializeField]
    public GameObject highScoreScreen;
    [SerializeField]
    public GameObject getReadyImage;
    [SerializeField]
    public GameObject JoinTheHighScoresScreen;
    [SerializeField]
    public GameObject PrivacyPolicyScreen;
    [SerializeField]
    public GameObject TermsOfServiceScreen;
    [SerializeField]
    public GameObject LoginScreen;
    [SerializeField]
    public GameObject ForgotPasswordScreen;

    public ObjectSpawn objectSpawner;

    private Score _score;
    private PlayFabManager _playFabManager;
    private List<MineMove> _mines = new List<MineMove>();
    private FishyMove _fishy;
    private bool isRunning = true;
    private ObjectSpawn _spawner;
    [SerializeField]
    private Rigidbody2D _octowussRb;


    private void Start()
    {
        gameOverScreen.SetActive(false);
        greatJobScreen.SetActive(false);
        awesomeJobScreen.SetActive(false);
        insaneJobScreen.SetActive(false);
        aboutScreen.SetActive(false);
        JoinTheHighScoresScreen.SetActive(false);
        getReadyImage.SetActive(true);
        Time.timeScale = 1;
        _score = FindObjectOfType<Score>();
        _playFabManager = FindObjectOfType<PlayFabManager>();
        _fishy = FindObjectOfType<FishyMove>();
        _spawner = FindObjectOfType<ObjectSpawn>();
    }

    private void Update()
    {
        if (!isRunning)
        {
            return;
        }

        for (int i = 0; i < _mines.Count; i++)
        {
            _mines[i].Move();
        }
        _fishy.Move();
        _spawner.Spawn();

    }

    public void GameOver()
    {
        isRunning = false;
        objectSpawner.GameOver();
        gameOverScreen.SetActive(true);
        getReadyImage.SetActive(false);
        _octowussRb.isKinematic = true;
        Time.timeScale = 0.4f;




        int score = _score.GetScore();

        Debug.Log("YOUR SCORE IS: " + score);

        //20
        if (score > 80f)
        {
            insaneJobScreen.SetActive(true);
        }
        else if (score > 50f)
        {
            awesomeJobScreen.SetActive(true);
        }
        else if (score > 15f)
        {
            greatJobScreen.SetActive(true);
        }
        Debug.Log(score);
        _playFabManager.SendLeaderboard(score);
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    public void AboutScreenClick()
    {
        aboutScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        getReadyImage.SetActive(false);
    }

    public void HighScoreScreenClick()
    {
        _playFabManager.GetLeaderboard();
        aboutScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        getReadyImage.SetActive(false);
        highScoreScreen.SetActive(true);
    }


    public void AddMine(MineMove _mine)
    {
        _mines.Add(_mine);
    }

    public void RemoveMine(MineMove _mine)
    {
        _mines.Remove(_mine);
    }
    public void OpenRegisterUser()
    {
        JoinTheHighScoresScreen.SetActive(true);
    }
    public void CloseRegisterUser()
    {
        JoinTheHighScoresScreen.SetActive(false);
    }
    public void OpenPrivacyPolicy()
    {
        PrivacyPolicyScreen.SetActive(true);
    }
    public void OpenTermsOfServicePolicy()
    {
        TermsOfServiceScreen.SetActive(true);
    }
    public void ClosePrivacyPolicy()
    {
        PrivacyPolicyScreen.SetActive(false);
    }
    public void CloseTermsOfServicePolicy()
    {
        TermsOfServiceScreen.SetActive(false);
    }
    public void OpenLoginScreen()
    {
        LoginScreen.SetActive(true);
    }
    public void CloseLoginScreen()
    {
        LoginScreen.SetActive(false);
    }
    public void OpenForgotPasswordScreen()
    {
        ForgotPasswordScreen.SetActive(true);
        LoginScreen.SetActive(false);
    }
    public void CloseForgetPasswordScreen()
    {
        ForgotPasswordScreen.SetActive(false);
    }
}

