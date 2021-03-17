using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovenmentInput { get; private set; }
    public int NormalInputX { get; private set; }
    public int NormalInputY { get; private set; }
    public bool JumpInput { get; private set; }

    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovenmentInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(RawMovenmentInput.x) > 0.5f)
        {
            NormalInputX = (int)(RawMovenmentInput * Vector2.right).normalized.x;
        }
        else
        {
            NormalInputX = 0;
        }
        if (Mathf.Abs(RawMovenmentInput.y) > 0.5f)
        { 
            NormalInputY = (int)(RawMovenmentInput * Vector2.up).normalized.y; 
        }
        else
        {
            NormalInputY = 0;
        }    

    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
       if(context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        } 

       if(context.canceled)
        {
            JumpInputStop = true;

        }
    }
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            GrabInput = true;
        }
        if(context.canceled)
        {
            GrabInput = false;
        }
    }
    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()   //este nevoie
    {
        if(Time.time>= jumpInputStartTime+inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
