using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput playerInput;
    public static Vector2 moveInput;
    public static bool JumpInputPressed;
    public static bool JumpInputHeld;
    public static bool JumpInputReleased;
    private InputAction moveAction;
    private InputAction jumpAction;

    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }


    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        JumpInputPressed = jumpAction.WasPressedThisFrame();
        JumpInputHeld = jumpAction.IsPressed();
        JumpInputReleased = jumpAction.WasReleasedThisFrame();
    }
}
