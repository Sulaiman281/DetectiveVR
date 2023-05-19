using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerInput.ActionEvent onSceneStart;

    [SerializeField] private PlayerInput.ActionEvent onUnityServiceConnected;
    
    private void Start()
    {
        onSceneStart.Invoke(default);
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
