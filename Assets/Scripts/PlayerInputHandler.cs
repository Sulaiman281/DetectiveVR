using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputAssetWithAction[] inputActions;


    private void Start()
    {
        foreach (var inputAssetWithAction in inputActions)
        {
            inputAssetWithAction.EnableInput();
            inputAssetWithAction.InvokeCancelInput(default);
        }
    }

    private void OnDestroy()
    {
        foreach (var inputAssetWithAction in inputActions)
        {
            inputAssetWithAction.DisableInput();
        }
    }
}

[Serializable]
public struct InputAssetWithAction
{
    [Header("Input Action")]
    public InputAction input;
    [Header("Events")]
    public PlayerInput.ActionEvent onStartInput, onCancelInput;

    public void EnableInput()
    {
        input.Enable();
        var tmpThis = this;
        tmpThis.input.started += InvokeStartInput;

        tmpThis.input.canceled += InvokeCancelInput;
    }

    public void InvokeStartInput(InputAction.CallbackContext callbackContext)
    {
        onStartInput.Invoke(default);
    }

    public void InvokeCancelInput(InputAction.CallbackContext callbackContext)
    {
        onCancelInput.Invoke(default);
    }

    public void DisableInput()
    {
        input.Disable();
        input.started -= InvokeStartInput;
        input.canceled -= InvokeCancelInput;
    }
}
