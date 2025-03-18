using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SwipeDetector : MonoBehaviour
{   
    #region Events

    public delegate void SwipeStart(Vector2 position, float time);
    public event SwipeStart OnSwipeStart;
    public delegate void SwipeEnd(Vector2 position, float time);
    public event SwipeEnd OnSwipeEnd;
    #endregion
    private Controls controls;
    void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Player.PrimaryContact.started += ctx => PrimaryStart(ctx);
        controls.Player.PrimaryContact.canceled += ctx => PrimaryEnd(ctx);
    }

    private void PrimaryStart(InputAction.CallbackContext context)
    {
        if(OnSwipeStart != null) OnSwipeStart(Util.ScreenToWorld(Camera.main, controls.Player.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void PrimaryEnd(InputAction.CallbackContext context)
    {
        if(OnSwipeEnd != null) OnSwipeEnd(Util.ScreenToWorld(Camera.main, controls.Player.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 TouchPosition()
    {
        return Util.ScreenToWorld(Camera.main, controls.Player.PrimaryPosition.ReadValue<Vector2>());
    }
}
