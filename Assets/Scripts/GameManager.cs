using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerInput.ActionEvent onSceneStart;
    [SerializeField] private PlayerInput.ActionEvent onBackToMenu;

    [SerializeField] private PlayerInput.ActionEvent onUnityServiceConnected;

    private void Start()
    {
        if (PlayerPrefs.HasKey("BTM"))
        {
            var value = PlayerPrefs.GetInt("BTM");
            if (value == 10)
            {
                onBackToMenu.Invoke(default);
                PlayerPrefs.SetInt("BTM", 0);
                PlayerPrefs.Save();
                return;
            }
        }
        onSceneStart.Invoke(default);
        // if (!PlayerPrefs.HasKey(DataKeys.TimelineMatrixKey))
        // {
        //     var matrics = new TimelineMatrics
        //     {
        //         IntroPlayed = false,
        //         Chapter1DialogPlayed = false
        //     };
        //     PlayerPrefs.SetString(DataKeys.TimelineMatrixKey, JsonUtility.ToJson(matrics));
        //     PlayerPrefs.Save();
        //     // var json = PlayerPrefs.GetString("TimelineMatrix");
        //     // var timeline = JsonUtility.FromJson<TimelineMatrics>(json);
        // }
    }

    public async void InitializeUnityService()
    {
        try
        {
            await UnityServices.InitializeAsync();

            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            onUnityServiceConnected.Invoke(default);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}