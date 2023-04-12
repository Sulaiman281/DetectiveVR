using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Chapter2Manager : Singleton<Chapter2Manager>
{
    [Header("Reference")] [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] destinationPoints;
    [SerializeField] private GameObject skipOption;
    [SerializeField] private Transform playerOrigin;
    [SerializeField] private ClueFinderGlass clueFinder;

    [Header("AudioSfx")]
    [SerializeField] private AudioClip driveBySfx;
    
    [Header("Actors")]
    [SerializeField] private AIAgent[] actors;
    [SerializeField] private AudioSource actorsAudio;

    [Header("Dialogs")]
    [SerializeField] private AudioClip[] dialogs;
    [SerializeField] private string[] subtitles;
    [SerializeField] private TMP_Text subtitlesTMP;
    [SerializeField] private AudioSource dialogSource;

    [Header("Environment")]
    [SerializeField] private Transform sunRotation;
    [SerializeField] private GameObject rainObject;

    [SerializeField] private TMP_Text tipText;

    [Header("SlowMotion")] [SerializeField]
    private InputAction slowMotionAction;

    [SerializeField] private float slowValue;
    [SerializeField] private int slowTimes;
    [SerializeField] private TMP_Text slowTMP;
    [SerializeField] private float slowDuration;
    private int _slowActionPerformed;

    [SerializeField] private PlayableDirector director;

    [Header("Game Timer")]
    [SerializeField] private int gameTimeSecond = 60;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TMP_Text timerText;
    private Coroutine _gameTimer;
    [SerializeField] private PlayerInput.ActionEvent onGameOver;

    [SerializeField] private int totalCluesToFind;
    private int _clueFounded;


    private List<Vector3> _spawnPositions = new();

    public Vector3 RandDestination => destinationPoints[Random.Range(0, destinationPoints.Length)].position;

    public bool enteredInCircle { get; set; }

    private int _foundClues;
    private void Start()
    {
        slowMotionAction.Enable();
        slowMotionAction.started += _ =>
        {
            if (_slowActionPerformed > slowTimes) return;
            StartCoroutine(SlowTime());
            _slowActionPerformed++;
            slowTMP.text = $"Press Left Trigger Key To Slow Down the Game \n ({_slowActionPerformed}/{slowTimes})";
        };
        SpawnActors();
        // slowMotionAction.canceled += _ => { Time.timeScale = 1; };
        sunRotation.Rotate(new Vector3(Random.Range(-180, 180), 0, 0));
        timerPanel.SetActive(false);
    }

    public void PauseDirectory()
    {
        if (enteredInCircle)
        {
            director.Play();
            return;
        }
        director.Pause();
    }

    public void PlayDialogs(int index)
    {
        if (index == 1)
        {
            slowTMP.text = $"Press Y Key To Slow Down the Game \n ({_slowActionPerformed}/{slowTimes})";
        }

        if (index == 2)
        {
            tipText.text = "Keep Your Eyes Open";
            rainObject.SetActive(Random.Range(0,2) == 1);
        }
        StartCoroutine(WriteText(subtitles[index]));
    }

    public void SpawnActors()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            _spawnPositions.Add(spawnPoint.position);
        }
        foreach (var actor in actors)
        {
            var randPos = Random.Range(0, _spawnPositions.Count);
            actor.transform.position = _spawnPositions[randPos];
            actor.gameObject.SetActive(true);
            _spawnPositions.RemoveAt(randPos);
            // actor.destination = destinationPoints[Random.Range(0, destinationPoints.Length)].position;
        }

        var suspectsCat = actors.Where(actor => actor.GetComponent<CluesOnBody>() != null).ToList();
        var suspect = suspectsCat[Random.Range(0, suspectsCat.Count)].GetComponent<CluesOnBody>();
        suspect.ShowClues();
    }

    public void GiveActorsDestinationToMove()
    {
        actorsAudio.Play();
        foreach (var actor in actors)
        {
            actor.destination = destinationPoints[Random.Range(0, destinationPoints.Length)].position;
        }
        director.Stop();
        skipOption.SetActive(false);
        // start the game timer
        timerPanel.SetActive(true);
        StartCoroutine(ChapterTimer());
    }

    private IEnumerator ChapterTimer()
    {
        for (var i = gameTimeSecond; i >= 0; i--)
        {
            if( i % 10 == 0) AudioSource.PlayClipAtPoint(driveBySfx, playerOrigin.position);
            yield return new WaitForSeconds(1);
            timerText.text = $"Time Left :({$"{i}".PadLeft(2, '0')})";
        }
        // on timer over.
        GameOver();
    }


    private IEnumerator SlowTime()
    {
        Time.timeScale = slowValue;
        yield return new WaitForSeconds(slowDuration);
        Time.timeScale = 1f;
    }

    public void OnClueFind()
    {
        if (_clueFounded++ >= totalCluesToFind)
        {
            // game over
            StopCoroutine(_gameTimer);
            GameOver();
        }
    }

    private void GameOver()
    {
        onGameOver.Invoke(default);
        // show exit door
    }

    private IEnumerator WriteText(string text)
    {
        subtitlesTMP.text = "";
        foreach (var t in text)
        {
            subtitlesTMP.text += t;
            yield return new WaitForSeconds(0.03f); // Change the duration to set the speed of the typing effect
        }

        Invoke(nameof(Clear), 2f);
    }

    public void Clear()
    {
        subtitlesTMP.text = "";
    }

    public void PlayAgain()
    {
        instance = null;
        SceneManager.LoadScene("Chapter2");
    }

    public void Menu()
    {
        instance = null;
        SceneManager.LoadScene("Menu");
    }
}