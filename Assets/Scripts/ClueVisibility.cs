using UnityEngine;

public class ClueVisibility : MonoBehaviour
{
    public bool isVisibleToEye;

    private void OnBecameVisible()
    {
        isVisibleToEye = true;
    }

    private void OnBecameInvisible()
    {
        isVisibleToEye = false;
    }
}
