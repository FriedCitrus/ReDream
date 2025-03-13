using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMoveStats")]

public class PlayerMoveStats : ScriptableObject
{
    [Header("Walk")]
    [Range(1f, 100f)] public float MaxwalkSpeed = 12.5f;
    [Range(1f, 100f)] public float GroundAcceleration = 5f;
    [Range(1f, 100f)] public float GroundDeceleration = 20f;
    [Range(1f, 100f)] public float AirAcceleration = 5f;
    [Range(1f, 100f)] public float AirDeceleration = 5f;

    [Header("Run")]
    [Range(1f, 100f)] public float MaxRunSpeed = 20f;

    [Header("Ground Check")]
    public LayerMask GroundLayer;
    public float GroundCheckDistance = 0.02f;
    public float HeadCheckDistance = 0.02f;
    [Range(0f, 1f)] public float HeadWidth = 0.75f;

    [Header("Jump")]
    public float jumpHeight =  6.5f;
    [Range(1f, 1.1f)] public float JumpCompensation = 1.05f;
    public float TimeTillApex = 0.35f;
    [Range(1f, 5f)] public float GravityReleaseMult = 2f;
    public float MaxFallSpeed = 26f;
    [Range(1, 5)] public int JumpsAllowed = 2;

    [Header("Jump Cut")]
    [Range(0.2f, 0.3f)] public float TimeforCancel = 0.27f;

    [Header("Jump Apex")]
    [Range (0.5f, 1f)] public float ApexThresh = 0.97f;
    [Range(0.01f, 1f)] public float ApexTime = 0.075f;

    [Header("Jump Buffer")]
    [Range(0f, 1f)] public float JumpBufferTime = 0.125f;

    [Header("Jump Coyote")]
    [Range(0f, 1f)] public float JumpCoyoteTime = 0.1f;

    [Header("Debug")]
    public bool DebugGroundCheckbox;
    public bool DebugHeadCheckbox;

    [Header("Jump Virtualization")]
    public bool ShowWalkJump = false;
    public bool StopOnCollision = true;
    public bool DrawRight = true;
    [Range(5,100)] public int JumpResolution = 30;
    [Range(0, 600)] public int VirtualizationSteps = 100;

    public float Gravity { get; private set; }
    public float JumpVelocity { get; private set; }
    public float AdjJumpHeight { get; private set; }

    public void Calculate(){
        AdjJumpHeight = jumpHeight * JumpCompensation;
        Gravity = -(2 * AdjJumpHeight) / Mathf.Pow(TimeTillApex, 2f);
        JumpVelocity = Mathf.Abs(Gravity) * TimeTillApex;
    }

    private void OnValidate() {
        Calculate();
    }

    private void OnEnable() {
        Calculate();
    }

}
