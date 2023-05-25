using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TimelineEvents : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private PlayerInput.ActionEvent onDirectoryFinished;

    [Header("Dialogs")] [SerializeField] private TMP_Text subtitlesTMP;
    [SerializeField] private string[] dialogsSubtitles;
    [SerializeField] private float[] dialogSkipSequence;

    private void OnEnable()
    {
        director.stopped += TriggerEvent;
    }

    private void OnDisable()
    {
        director.stopped -= TriggerEvent;
    }

    public int dialogIndex = -1;

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
            yield return new WaitForSeconds(0.03f); // Change the duration to set the speed of the typing effect
        }
    }

    public void ClearSubtitles()
    {
        subtitlesTMP.text = "";
    }

    public void SkipDialog()
    {
        // PlayDialog();
        if (dialogIndex >= dialogSkipSequence.Length)
        {
            director.time = director.duration-0.1f;
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
}
