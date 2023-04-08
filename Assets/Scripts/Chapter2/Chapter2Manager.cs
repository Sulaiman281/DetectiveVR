using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Transform playerOrigin;
    
    [Header("Actors")]
    [SerializeField] private Transform actorsParent;
    [SerializeField] private GameObject[] actorsPrefab;
    [SerializeField] private int totalActors;

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

    private List<Vector3> _spawnPositions = new();
    private List<AIAgent> _aiAgent = new();

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

        for (var i = 0; i < Math.Min(totalActors, actorsPrefab.Length); i++)
        {
            var randPos = Random.Range(0, _spawnPositions.Count);
            var agent = Instantiate(actorsPrefab[i], _spawnPositions[randPos],
                Quaternion.identity).GetComponent<AIAgent>();
            agent.transform.SetParent(actorsParent);
            _spawnPositions.RemoveAt(randPos);
            _aiAgent.Add(agent);

            agent.destination = destinationPoints[Random.Range(0, destinationPoints.Length)].position;
        }
    }

    private IEnumerator SlowTime()
    {
        Time.timeScale = slowValue;
        yield return new WaitForSeconds(slowDuration);
        Time.timeScale = 1f;
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

    public void TimeUp()
    {
        // lost the suspect time up
    }

    public void Menu()
    {
        instance = null;
        SceneManager.LoadScene("Menu");
    }
}