using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerInput.ActionEvent onPlayerStart;

    [SerializeField] private GameObject inventory;

    private void Start()
    {
        onPlayerStart.Invoke(default);
    }

    public void ToggleInventory(bool value)
    {
        if (!value)
            StartCoroutine(DelayAction(() => { inventory.SetActive(false); }, 1.5f));
        else
        {
            inventory.SetActive(true);
        }
    }

    public IEnumerator DelayAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    public void ChangeScene(int sceneIndex)
    {
        try
        {
            SceneManager.LoadScene(sceneIndex);
        }
        catch(Exception)
        {
            // ignore
        }
    }
}