using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private void OnEnable()
    {
        if (Camera.main == null) return;
        transform.LookAt(Camera.main.transform.position);
    }
}
