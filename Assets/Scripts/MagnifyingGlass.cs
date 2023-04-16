using UnityEngine;

public class MagnifyingGlass : MonoBehaviour
{
    public float maxDistance = 10f;  // Maximum distance to check for objects
    public LayerMask objectLayerMask;  // Layer mask to filter objects
    public float actionDistance = 3f;  // Distance threshold to trigger action
    public float viewAngle = 45f;  // Field of view angle of the camera
    public Transform player;  // Player's transform reference for calculating view angle

    [SerializeField] private Camera lensCam;
    [SerializeField] private Transform rayTarget;

    void Update()
    {
        if (Physics.Raycast(new Ray(rayTarget.position, rayTarget.forward * maxDistance), out var hitInfo, maxDistance, objectLayerMask))
        {
            var trans = hitInfo.collider.transform;
            var dot = Vector3.Dot(rayTarget.forward, trans.forward);
            if(dot < -0.9)
                Debug.Log("Objects are facing towards each other");
            Debug.Log(hitInfo.collider.name+" Found a Clue");
        }
        // // Calculate the direction from the magnifying glass camera to the player's position
        // Vector3 playerDirection = player.position - transform.position;
        // float distanceToPlayer = playerDirection.magnitude;
        //
        // // Check if the player is within the maximum distance and view angle
        // if (distanceToPlayer <= maxDistance && Vector3.Angle(transform.forward, playerDirection) <= viewAngle / 2f)
        // {
        //     // Cast a ray from the magnifying glass camera towards the player's position
        //     Ray ray = new Ray(transform.position, playerDirection);
        //     RaycastHit hit;
        //
        //     if (Physics.Raycast(ray, out hit, maxDistance, objectLayerMask))
        //     {
        //         // Check if the ray hit an object with ClueObject script
        //         ClueObject clue = hit.collider.GetComponent<ClueObject>();
        //         if (clue != null)
        //         {
        //             // Check if the object is within action distance
        //             if (distanceToPlayer <= actionDistance)
        //             {
        //                 // Perform action
        //                 Debug.Log("Object with ClueObject script is in view!");
        //                 // Perform your desired action here, e.g. displaying a clue, playing a sound, etc.
        //             }
        //         }
        //     }
        // }
    }


    void OnDrawGizmos()
    {
        // Draw wire sphere to represent the maximum distance for checking objects
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(rayTarget.position, rayTarget.forward * maxDistance);
        // Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
