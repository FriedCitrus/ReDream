using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput playerInput;

    public static Vector2 moveInput;
    //public static bool JumpInput;

    private InputAction moveAction;
    //private InputAction jumpAction;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        //jumpAction = playerInput.Player.Jump;
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.x > 0)
        {
            Debug.Log("Right");
        }
        else if (moveInput.x < 0)
        {
            Debug.Log("Left");
        }
        //JumpInput = jumpAction.wasPressedThisFrame;
    }
}
