using UnityEngine;

public class FixCamera : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        if(canvas.worldCamera == null) canvas.worldCamera = Camera.main;
    }

    private void OnValidate()
    {
        if (canvas == null) canvas = transform.GetComponent<Canvas>();
        if(canvas.worldCamera == null) canvas.worldCamera = Camera.main;
    }
}
