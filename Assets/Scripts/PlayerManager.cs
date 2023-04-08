using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private InputAction leftPrimaryKeyPressed;
    
    [Header("References")]
    [SerializeField] private GameObject handCanvas;

    private void Awake()
    {
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
    }
}
