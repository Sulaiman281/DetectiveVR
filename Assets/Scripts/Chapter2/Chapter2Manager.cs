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
    private int _slowActionPerformed;

    [SerializeField] private PlayableDirector director;

    private List<Vector3> _spawnPositions = new();
    private List<AIAgent> _aiAgent = new();

    public Vector3 RandDestination => destinationPoints[Random.Range(0, destinationPoints.Length)].position;

    public bool enteredInCircle { get; set; }

    private void Start()
    {
        slowMotionAction.Enable();
        slowMotionAction.started += _ =>
        {
            if (_slowActionPerformed > slowTimes) return;
            StartCoroutine(SlowTime(.35f));
            _slowActionPerformed++;
            slowTMP.text = $"Press Y Key To Slow Down the Game \n ({_slowActionPerformed}/{slowTimes})";
        };
        slowMotionAction.canceled += _ => { Time.timeScale = 1; };
        SpawnActors();
        sunRotation.Rotate(new Vector3(Random.Range(-180, 180), 0, 0));

        // StartCoroutine(DelayMethod(()=>
        // {
        //     PlayDialogs(0);
        //     tipText.text = "Go To The Circle";
        // }, 2));
    }

    // private IEnumerator DelayMethod(Action action, float delay)
    // {
        // yield return new WaitForSeconds(delay);
        // action.Invoke();
    // }

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
        // dialogSource.clip = dialogs[index];
        // dialogSource.Play();
        if (index == 1)
        {
            slowTMP.text = $"Press Y Key To Slow Down the Game \n ({_slowActionPerformed}/{slowTimes})";
        }

        if (index == 2)
        {
            // StartCoroutine(DelayMethod(() =>
            // {
                // PlayDialogs(3);
            // }, dialogs[index].length+2));
            tipText.text = "Keep Your Eyes Open";
            // AudioSource.PlayClipAtPoint(sirenSfx, playerOrigin.position);
            rainObject.SetActive(Random.Range(0,2) == 1);
        }
        StartCoroutine(WriteText(subtitles[index]));
    }

    private IEnumerator SlowTime(float byValue, bool doSlow = true)
    {
        if (doSlow)
        {
            while (Time.timeScale > slowValue)
            {
                Time.timeScale -= byValue;
                yield return new WaitForSeconds(0.009f);
            }
        }
        else
        {
            while (Time.timeScale < 1)
            {
                Time.timeScale += byValue;
                yield return new WaitForSeconds(0.009f);
            }
        }
    }

    private void SpawnActors()
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

    // private int _index;
    //
    // public void ChangeText(int x)
    // {
    //     _index += x;
    //     if (_index >= subtitles.Length) _index = 0;
    //     StartCoroutine(WriteText(subtitles[_index]));
    // }

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

    public void Menu()
    {
        instance = null;
        SceneManager.LoadScene("Menu");
    }
}