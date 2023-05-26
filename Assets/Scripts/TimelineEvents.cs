using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineEvents : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    
    [Header("Events")]
    [SerializeField] private PlayerInput.ActionEvent onIntroPlayingForFirstTime;
    [SerializeField] private PlayerInput.ActionEvent onIntroPlayingMoreThenOne;
    [SerializeField] private PlayerInput.ActionEvent onDirectoryFinished;

    [Header("Dialogs")] [SerializeField] private TMP_Text subtitlesTMP;
    [SerializeField] private string[] dialogsSubtitles;
    [SerializeField] private float[] dialogSkipSequence;
    [SerializeField] private float subtitleWritingSpeed = 0.03f;
    public int dialogIndex;
    
    [Header("Debug")]
    public bool introDebugging;

    public void StartTimeline()
    {
        CheckHavePlayedIntro();
    }

    private void OnEnable()
    {
        director.stopped += TriggerEvent;
    }

    private void OnDisable()
    {
        director.stopped -= TriggerEvent;
    }

    #region Subtitles of Dialogs
    
    public void PlayDialog()
    {
        if (dialogIndex >= dialogsSubtitles.Length) return;
        StopAllCoroutines();
        StartCoroutine(WriteText(dialogsSubtitles[dialogIndex]));
        dialogIndex++;
    }

    private IEnumerator WriteText(string text)
    {
        subtitlesTMP.text = "";
        foreach (var t in text)
        {
            subtitlesTMP.text += t;
            yield return new WaitForSeconds(subtitleWritingSpeed); // Change the duration to set the speed of the typing effect
        }
    }

    public void ClearSubtitles()
    {
        if (subtitlesTMP == null) return;
        subtitlesTMP.text = "";
    }
    
    #endregion


    public void SkipDialog()
    {
        // PlayDialog();
        if (dialogIndex >= dialogSkipSequence.Length)
        {
            director.time = director.duration-0.1f;
            ClearSubtitles();
            return;
        }
        director.time = dialogSkipSequence[dialogIndex];
    }

    private void OnDestroy()
    {
        director.stopped -= TriggerEvent;
    }

    private void TriggerEvent(PlayableDirector obj)
    {
        onDirectoryFinished.Invoke(default);
    }

    private void OnValidate()
    {
        if (director == null) director = GetComponent<PlayableDirector>();
    }

    #region Checking Scene Intro Matrics

    private void CheckHavePlayedIntro()
    {
        var playedTimes = GetPlayedTimes();
        if (playedTimes > 0 && !introDebugging)
        {
            // invoke intro has played before event
            onIntroPlayingMoreThenOne.Invoke(default);
        }
        else
        {
            onIntroPlayingForFirstTime.Invoke(default);
            // invoke intro playing first time event
        }
    }

    public void PlayDirectory(PlayableAsset asset)
    {
        Debug.Log(director.isActiveAndEnabled+" Timeline");
        if (!director.isActiveAndEnabled)
        {
            director.gameObject.SetActive(true);
            director.enabled = true;
        }
        var key = SceneManager.GetActiveScene().name + "-Intro";
        var timePlayed = 0;
        if (!PlayerPrefs.HasKey(key))
        {
            timePlayed = PlayerPrefs.GetInt(key);
        }
        director.Play(asset);
        timePlayed++;
        PlayerPrefs.SetInt(key, timePlayed);
        PlayerPrefs.Save();
        Debug.Log(director.state+" directory "+director.time);
    }

    private int GetPlayedTimes()
    {
        var key = SceneManager.GetActiveScene().name + "-Intro";
        return !PlayerPrefs.HasKey(key) ? 0 : PlayerPrefs.GetInt(key);
    }

    #endregion
}
