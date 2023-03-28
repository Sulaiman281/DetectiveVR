using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private InputAction leftPrimaryKeyPressed;
    [Header("References")]
    [SerializeField] private GameObject handCanvas;

    [SerializeField] private GameObject rightRayController;
    [SerializeField] private GameObject rightDirectController;

    private void Awake()
    {
        Chapter1Manager.instance.playerManager = this;
        leftPrimaryKeyPressed.Enable();
        leftPrimaryKeyPressed.started += _ =>
        {
            ShowCanvas(true);
        };
        leftPrimaryKeyPressed.canceled += _ =>
        {
            ShowCanvas(false);
        };
    }

    public void ShowCanvas(bool value)
    {
        handCanvas.SetActive(value);
        rightDirectController.SetActive(!value);
        rightRayController.SetActive(value);
    }
}
