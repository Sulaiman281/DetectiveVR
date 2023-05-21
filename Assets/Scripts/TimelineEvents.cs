using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TimelineEvents : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private PlayerInput.ActionEvent onDirectoryFinished;

    private void OnEnable()
    {
        director.stopped += TriggerEvent;
    }

    private void OnDisable()
    {
        director.stopped -= TriggerEvent;
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
