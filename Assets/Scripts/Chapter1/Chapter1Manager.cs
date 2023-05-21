using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chapter1Manager : Singleton<Chapter1Manager>
{
    [SerializeField] private PlayerInput.ActionEvent onSceneStart;
    [SerializeField] private PlayerInput.ActionEvent onGameFail;
    [SerializeField] private PlayerInput.ActionEvent onGameSuccess;

    private void Start()
    {
        onSceneStart.Invoke(default);
        
        startTimer = false;
    }

    private void Update()
    {
        UpdateTimer();
    }

    #region Timer

    [Header("Timer")] [SerializeField] private TMP_Text timerText;
    public float seconds;

    public bool startTimer { get; set; }

    private void UpdateTimer()
    {
        if (!startTimer) return;
        seconds -= Time.deltaTime;
        timerText.text = $"Timer: {(int)seconds / 60}:{(int)seconds % 60}";
        if (!(seconds <= 0)) return;
        onGameFail.Invoke(default);
        startTimer = false;
    }

    #endregion

    #region Clues Checker

    [Header("Clues")] [SerializeField] private MagnifyingGlass lense;
    [SerializeField] private TMP_Text clueText;

    public int totalClues;
    public int clueSolved;
    public bool canCheckClues { get; set; }

    private List<ClueObject> clues;

    public void ClueSetup()
    {
        clues = FindObjectsByType<ClueObject>(FindObjectsSortMode.None).ToList();
        totalClues = clues.Count;
        clueText.text = $"Find Clues ({clueSolved}/{totalClues}";
        // Debug.Log(clues.Count);
    }

    public void CheckClue()
    {
        if (!canCheckClues) return;
        lense.CheckClue();
    }

    public void FoundClue()
    {
        clueSolved = clues.Where(clue => clue.solved).ToList().Count;
        clueText.text = $"Find Clues ({clueSolved}/{totalClues}";
    }

    public void SuspectDecision(bool guilty)
    {
        if (clueSolved < 2)
        {
            onGameFail.Invoke(default);
        }
        else
        {
            if (guilty)
            {
                onGameSuccess.Invoke(default);
            }
            else
            {
                onGameFail.Invoke(default);
            }
        }
    }

    #endregion


    #region Dialogs

    [Header("Dialogs")] [SerializeField] private TMP_Text subtitlesTMP;
    [SerializeField] private string[] dialogsSubtitles;

    private int _dialogIndex = -1;

    public void PlayDialog()
    {
        _dialogIndex++;
        if (_dialogIndex >= dialogsSubtitles.Length) return;
        StartCoroutine(WriteText(dialogsSubtitles[_dialogIndex]));
    }

    private IEnumerator WriteText(string text)
    {
        subtitlesTMP.text = "";
        foreach (var t in text)
        {
            subtitlesTMP.text += t;
            yield return new WaitForSeconds(0.03f); // Change the duration to set the speed of the typing effect
        }
    }

    public void ClearSubtitles()
    {
        subtitlesTMP.text = "";
    }

    #endregion

    #region background music

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic;

    public void PlayBgMusic()
    {
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    #endregion
}