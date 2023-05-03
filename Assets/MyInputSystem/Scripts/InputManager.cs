using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private InputActions _inputActions;

    public HandController leftController;
    public HandController rightController;

    protected override void Awake()
    {
        base.Awake();
        _inputActions = new InputActions();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void Update()
    {
        { // left controller
            leftController.joystickMove = _inputActions.LeftHand.JoyStickMove.ReadValue<Vector2>();
            leftController.menuPress = _inputActions.LeftHand.Menu.triggered;
            
            leftController.gripPressedHold = _inputActions.LeftHand.GripPressHold.triggered;
            leftController.gripPressedTap = _inputActions.LeftHand.GripPressTap.triggered;
            leftController.gripPressedMultiTap = _inputActions.LeftHand.GripPressMultiTap.triggered;
            
            leftController.triggerPressedHold = _inputActions.LeftHand.TriggerPressHold.triggered;
            leftController.triggerPressedTap = _inputActions.LeftHand.TriggerPressedTap.triggered;
            leftController.triggerPressedMultiTap = _inputActions.LeftHand.TriggerPressedTap.triggered;
            
            leftController.primaryPressedHold = _inputActions.LeftHand.PrimaryPressHold.triggered;
            leftController.primaryPressedTap = _inputActions.LeftHand.PrimaryPressTap.triggered;
            leftController.primaryPressedMultiTap = _inputActions.LeftHand.PrimaryPressMultiTap.triggered;
            
            leftController.primaryTouchHold = _inputActions.LeftHand.PrimaryTouchHold.triggered;
            leftController.primaryTouchTap = _inputActions.LeftHand.PrimaryTouchTap.triggered;
            leftController.primaryTouchMultiTap = _inputActions.LeftHand.PrimaryTouchMultiTap.triggered;
            
            leftController.secondaryPressedHold = _inputActions.LeftHand.SecondaryPressedHold.triggered;
            leftController.secondaryPressedTap = _inputActions.LeftHand.SecondaryPressedTap.triggered;
            leftController.secondaryPressedMultiTap = _inputActions.LeftHand.SecondaryPressedMultiTap.triggered;
            
            leftController.secondaryTouchHold = _inputActions.LeftHand.SecondaryTouchHold.triggered;
            leftController.secondaryTouchTap = _inputActions.LeftHand.SecondaryTouchTap.triggered;
            leftController.secondaryTouchMultiTap = _inputActions.LeftHand.SecondaryTouchMultiTap.triggered;
        }
        { // right controller
            rightController.joystickMove = _inputActions.RightHand.JoyStickMove.ReadValue<Vector2>();
            rightController.menuPress = _inputActions.RightHand.Menu.triggered;
            
            rightController.gripPressedHold = _inputActions.RightHand.GripPressHold.triggered;
            rightController.gripPressedTap = _inputActions.RightHand.GripPressTap.triggered;
            rightController.gripPressedMultiTap = _inputActions.RightHand.GripPressMultiTap.triggered;
            
            rightController.triggerPressedHold = _inputActions.RightHand.TriggerPressHold.triggered;
            rightController.triggerPressedTap = _inputActions.RightHand.TriggerPressedTap.triggered;
            rightController.triggerPressedMultiTap = _inputActions.RightHand.TriggerPressedTap.triggered;
            
            rightController.primaryPressedHold = _inputActions.RightHand.PrimaryPressHold.triggered;
            rightController.primaryPressedTap = _inputActions.RightHand.PrimaryPressTap.triggered;
            rightController.primaryPressedMultiTap = _inputActions.RightHand.PrimaryPressMultiTap.triggered;
            
            rightController.primaryTouchHold = _inputActions.RightHand.PrimaryTouchHold.triggered;
            rightController.primaryTouchTap = _inputActions.RightHand.PrimaryTouchTap.triggered;
            rightController.primaryTouchMultiTap = _inputActions.RightHand.PrimaryTouchMultiTap.triggered;
            
            rightController.secondaryPressedHold = _inputActions.RightHand.SecondaryPressedHold.triggered;
            rightController.secondaryPressedTap = _inputActions.RightHand.SecondaryPressedTap.triggered;
            rightController.secondaryPressedMultiTap = _inputActions.RightHand.SecondaryPressedMultiTap.triggered;
            
            rightController.secondaryTouchHold = _inputActions.RightHand.SecondaryTouchHold.triggered;
            rightController.secondaryTouchTap = _inputActions.RightHand.SecondaryTouchTap.triggered;
            rightController.secondaryTouchMultiTap = _inputActions.RightHand.SecondaryTouchMultiTap.triggered;
        }
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
}

[Serializable]
public struct HandController
{
    // [Header("InputActions")]
    // [SerializeField] private Hand hand;
    // [SerializeField] private InputAction triggerTouchInput;
    // [SerializeField] private InputAction triggerPressedInput;
    // [SerializeField] private InputAction gripTouchInput;
    // [SerializeField] private InputAction gripPressedInput;
    // [SerializeField] private InputAction primaryTouchInput;
    // [SerializeField] private InputAction primaryPressedInput;
    // [SerializeField] private InputAction secondaryTouchInput;
    // [SerializeField] private InputAction secondaryPressedInput;
    // [SerializeField] private InputAction joyStickTouchInput;
    // [SerializeField] private InputAction joyStickPressInput;
    // [SerializeField] private InputAction joyStickMoveInput;
    // [SerializeField] private InputAction menuPressInput;

    [Header("InputValues")]
    public Vector2 joystickMove;
    public bool triggerTouchTap;
    public bool triggerTouchHold;
    public bool triggerTouchMultiTap;
    public bool triggerPressedTap;
    public bool triggerPressedHold;
    public bool triggerPressedMultiTap;
    public bool gripPressedTap;
    public bool gripPressedHold;
    public bool gripPressedMultiTap;
    public bool primaryTouchTap;
    public bool primaryTouchHold;
    public bool primaryTouchMultiTap;
    public bool primaryPressedTap;
    public bool primaryPressedHold;
    public bool primaryPressedMultiTap;
    public bool secondaryTouchTap;
    public bool secondaryTouchHold;
    public bool secondaryTouchMultiTap;
    public bool secondaryPressedTap;
    public bool secondaryPressedHold;
    public bool secondaryPressedMultiTap;
    public bool menuPress;

    public void UpdateAction()
    {
        
    }

    // public void EnableHand()
    // {
    //     triggerTouchInput.Enable();
    //     triggerPressedInput.Enable();
    //     gripTouchInput.Enable();
    //     gripPressedInput.Enable();
    //     primaryTouchInput.Enable();
    //     primaryPressedInput.Enable();
    //     secondaryTouchInput.Enable();
    //     secondaryPressedInput.Enable();
    //     joyStickTouchInput.Enable();
    //     joyStickPressInput.Enable();
    //     joyStickMoveInput.Enable();
    //     menuPressInput.Enable();
    // }
    //
    // public void DisableHand()
    // {
    //     triggerTouchInput.Disable();
    //     triggerPressedInput.Disable();
    //     gripTouchInput.Disable();
    //     gripPressedInput.Disable();
    //     primaryTouchInput.Disable();
    //     primaryPressedInput.Disable();
    //     secondaryTouchInput.Disable();
    //     secondaryPressedInput.Disable();
    //     joyStickTouchInput.Disable();
    //     joyStickPressInput.Disable();
    //     joyStickMoveInput.Disable();
    //     menuPressInput.Disable();
    // }
    //
    // public void Update()
    // {
    //     triggerTouch = triggerTouchInput.triggered;
    //     triggerPressed = triggerPressedInput.triggered;
    //     gripTouch = gripTouchInput.triggered;
    //     gripPressed = gripPressedInput.triggered;
    //     primaryTouch = primaryTouchInput.triggered;
    //     primaryPressed = primaryPressedInput.triggered;
    //     secondaryTouch = secondaryTouchInput.triggered;
    //     secondaryPressed = secondaryPressedInput.triggered;
    //     joyStickTouch = joyStickTouchInput.triggered;
    //     joyStickPress = joyStickPressInput.triggered;
    //     joystickMove = joyStickMoveInput.ReadValue<Vector2>();
    //     menuPress = menuPressInput.triggered;
    // }
}

public enum Hand
{
    LeftHand = 0,
    RightHand = 1
}