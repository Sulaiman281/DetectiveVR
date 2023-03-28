using System;
using UnityEngine;
using Random = System.Random;

public class VictomClues : MonoBehaviour
{
    [Header("reference")]
    [SerializeField] private Texture[] clues;
    [SerializeField] private Material bodyMat;

    [SerializeField] private ClueObject[] cluesGameObjects;

    [Serializable]
    struct ClueObject
    {
        public GameObject[] objects;
    }

    private void Awake()
    {
        foreach (var cluesGameObject in cluesGameObjects)
        {
            foreach (var o in cluesGameObject.objects)
            {
                o.SetActive(false);
            }
        }
    }

    private void Start()
    {
        var randNum = new Random().Next(0, clues.Length);
        bodyMat.mainTexture = clues[randNum];
        Chapter1Manager.instance.totalClues = cluesGameObjects[randNum].objects.Length;
        foreach (var t in cluesGameObjects[randNum].objects)
        {
            t.SetActive(true);
        }
    }
}
