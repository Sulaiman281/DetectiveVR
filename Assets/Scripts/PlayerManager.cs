using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerInput.ActionEvent onPlayerStart;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject inventorySockets;
    [SerializeField] private GameObject options;
    public float inventoryHideDelay { get; set; }

    private void Start()
    {
        onPlayerStart.Invoke(default);
    }

    public void ToggleInventory(bool value)
    {
        if (!value)
            StartCoroutine(DelayAction(() =>
            {
                inventoryUI.SetActive(false);
                inventorySockets.SetActive(false);
                options.SetActive(false);
            }, inventoryHideDelay));
        else
        {
            inventoryUI.SetActive(true);
            inventorySockets.SetActive(true);
            options.SetActive(true);
        }
    }

    public IEnumerator DelayAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    public void BackToMindPalace()
    {
        PlayerPrefs.SetInt("BTM", 10);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("Scenes/MindPalace");
    }

    public void ChangeScene(string sceneName)
    {
        ToggleInventory(true);
        StartCoroutine(DelayAction(() =>
        {
            try
            {
                var operation = SceneManager.LoadSceneAsync("Scenes/SceneLoader");
                operation.completed += asyncOperation =>
                {
                    var sceneLoader = FindObjectOfType<SceneLoader>();
                    if (sceneLoader != null)
                    {
                        var task = sceneLoader.NextScene(sceneName); // Store the returned Task
                        // Handle the task as needed
                    }
                    else
                    {
                        Debug.LogError("SceneLoader not found in the SceneLoader scene!");
                    }
                };
            }
            catch (Exception exception)
            {
                Debug.LogError("An error occurred while changing the scene: " + exception.Message);
            }
        }, .5f));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}