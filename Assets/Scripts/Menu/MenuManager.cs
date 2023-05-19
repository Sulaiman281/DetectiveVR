using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private VideoPlayer logoPlayer;
    [SerializeField] private PlayerInput.ActionEvent onLogoPlayingComplete;

    private void Start()
    {
        logoPlayer.Play();
        Invoke(nameof(LogoFinished), (float)logoPlayer.clip.length);
    }

    private void LogoFinished()
    {
        onLogoPlayingComplete.Invoke(default);
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
