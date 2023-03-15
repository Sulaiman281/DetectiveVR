using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        InitSound();
        InitLevels();

        
    }

    #region Menu SoundFX

    [Header("MenuSound")]
    [SerializeField] private AudioClip buttonSound;

    private void InitSound()
    {
        var buttons = FindObjectsOfType<Button>(true);
        foreach (var button in buttons)
        {
            button.onClick.AddListener(PlaySound);
        }
    }

    private void PlaySound()
    {
        if (Camera.main != null) AudioSource.PlayClipAtPoint(buttonSound, Camera.main.transform.position);
    }

    #endregion

    #region Level

    [Header("Level References")] [SerializeField]
    private Transform content;

    [SerializeField] private GameObject levelUIPrefab;

    private void InitLevels()
    {
        // load levels into UI
    }

    #endregion
    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
