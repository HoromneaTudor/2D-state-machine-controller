using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    private float longIdleTimer = 5f;
    private float timer;

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);
        //timer = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("longIdle", false);
        timer = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(xInput!=0 && !isExitingState)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        if(timer>=longIdleTimer)
        {
            player.Anim.SetBool("longIdle",true);
        }
        timer += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
