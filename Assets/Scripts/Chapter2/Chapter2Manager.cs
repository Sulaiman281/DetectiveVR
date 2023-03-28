using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Chapter2Manager : Singleton<Chapter2Manager>
{
    [Header("Reference")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] destinationPoints;

    [SerializeField] private GameObject[] actorsPrefab;
    [SerializeField] private int totalActors;

    [SerializeField] private string[] subtitles;
    [SerializeField] private TMP_Text subtitlesTMP;

    [SerializeField] private InputAction slowMotionAction;

    private List<Vector3> _spawnPositions = new();
    private List<AIAgent> _aiAgent = new();

    public Vector3 RandDestination => destinationPoints[Random.Range(0, destinationPoints.Length)].position;

    private void Start()
    {
        slowMotionAction.Enable();
        slowMotionAction.started += _ =>
        {

        };
        slowMotionAction.canceled += _ =>
        {
            
        };
        SpawnActors();
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
            _spawnPositions.RemoveAt(randPos);
            _aiAgent.Add(agent);

            agent.destination = destinationPoints[Random.Range(0, destinationPoints.Length)].position;
        }
    }

    private int _index;
    public void ChangeText(int x)
    {
        _index += x;
        if (_index >= subtitles.Length) _index = 0;
        StartCoroutine(WriteText(subtitles[_index]));
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
