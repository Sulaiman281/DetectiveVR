using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed;
    private void LateUpdate()
    {
        transform.Rotate(rotationSpeed, Space.Self);
    }
}
