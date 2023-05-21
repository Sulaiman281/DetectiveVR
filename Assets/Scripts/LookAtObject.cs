using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 1f;

    private void LateUpdate()
    {
        if (target == null) return;

        // Get the target's position without changing the object's position
        var targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);

        // Get the direction from the object's position to the target's position
        var direction = targetPosition - transform.position;

        // Calculate the target rotation based on the direction
        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Smoothly rotate the object towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}
