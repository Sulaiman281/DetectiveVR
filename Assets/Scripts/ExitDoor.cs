using UnityEngine;
using UnityEngine.InputSystem;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private PlayerInput.ActionEvent onExitDoorEnter;

    [SerializeField] private Transform doorFrame;
    [SerializeField] private Quaternion openRot;
    [SerializeField] private Quaternion closeRot;

    public bool isDoorOpen;

    private void OnValidate()
    {
        doorFrame.localRotation = isDoorOpen ? openRot : closeRot;
    }

    public void OpenDoor(bool value)
    {
        isDoorOpen = value;
        doorFrame.localRotation = isDoorOpen ? openRot : closeRot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        onExitDoorEnter.Invoke(default);
    }
}