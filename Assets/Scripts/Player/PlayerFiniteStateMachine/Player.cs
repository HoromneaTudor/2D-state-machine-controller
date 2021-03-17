using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variable
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }

    public PlayerWallJumpState WallJumpState { get; private set; }

    [SerializeField]
    private PlayerData _playerdata;

    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    #endregion

    #region Chech Transforms

    [SerializeField]
    private Transform _groundCheck;

    [SerializeField]
    private Transform _wallCheck;

    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _playerdata, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, _playerdata, "move");
        JumpState = new PlayerJumpState(this, StateMachine, _playerdata, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, _playerdata, "inAir");
        LandState = new PlayerLandState(this, StateMachine, _playerdata, "land");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, _playerdata, "wallClimb");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, _playerdata, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, _playerdata, "wallGrab");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, _playerdata, "inAir");

    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rb = GetComponent<Rigidbody2D>();

        FacingDirection = 1;

        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        CurrentVelocity = Rb.velocity; //inainte era aici dar cred ca ar trebui sa fie in FixedUpdate
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        //CurrentVelocity = Rb.velocity;//aici cred ca ar trebui
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity,Vector2 angle,int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _playerdata.wallCheckDistance,_playerdata.whatIsGround);
    }
    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * -FacingDirection, _playerdata.wallCheckDistance, _playerdata.whatIsGround);
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _playerdata.groundCheckRadius, _playerdata.whatIsGround);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _playerdata.groundCheckRadius);
    }*/
    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180f, 0.0f);
    }
    #endregion
}
