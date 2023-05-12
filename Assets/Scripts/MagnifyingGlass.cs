using UnityEngine;
using UnityEngine.InputSystem;

public class MagnifyingGlass : MonoBehaviour
{
    [SerializeField] private Transform rayTarget;
    [SerializeField] private Vector3 raySize;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private AudioClip clueSound;

    public InputAction actionInput;

    private void Start()
    {
        actionInput.Enable();
        actionInput.performed += context =>
        {
            CheckClue();
        };
    }

    public void CheckClue()
    {
        var results = new Collider[10];
        Physics.OverlapBoxNonAlloc(rayTarget.TransformPoint(posOffset), raySize, results, rayTarget.rotation);
        foreach (var collider in results)
        {
            if(collider == null) continue;
            if (collider.CompareTag("Clue"))
            {
                Debug.Log("Clue Solved");
                collider.transform.GetComponent<ClueObject>().solved = true;
                AudioSource.PlayClipAtPoint(clueSound, transform.position, .8f);
                break;
            }
        }
    }

    //
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireCube(rayTarget.TransformPoint(posOffset), raySize);
    // }
}