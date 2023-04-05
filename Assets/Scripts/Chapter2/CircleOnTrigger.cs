using UnityEngine;
using UnityEngine.InputSystem;

public class CircleOnTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInput.ActionEvent action;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        action.Invoke(default);
    }
}
