using System.Collections;
using TMPro;
using UnityEngine;

public class Chapter1Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text subtitlesTMP;
    [SerializeField] private string[] dialogsSubtitles;

    private int _index = -1;

    public bool findClueStart { get; set; }

    public void ChangeText(int x)
    {
        _index += x;
        if (_index >= dialogsSubtitles.Length) _index = 0;
        StartCoroutine(WriteText(dialogsSubtitles[_index]));
    }

    private IEnumerator WriteText(string text)
    {
        subtitlesTMP.text = "";
        foreach (var t in text)
        {
            subtitlesTMP.text += t;
            yield return new WaitForSeconds(0.04f); // Change the duration to set the speed of the typing effect
        }
    }

    public void Clear()
    {
        subtitlesTMP.text = "";
    }
}