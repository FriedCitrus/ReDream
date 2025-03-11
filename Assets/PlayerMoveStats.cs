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
    public float GroundCheckDistance = 0.2f;
    public float HeadCheckDistance = 0.2f;
    [Range(0f, 1f)] public float HeadWidth = 0.5f;
    //[Header("Jump")]

}
