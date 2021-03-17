using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate() //cred ca ar trebui in physics update
    {
        base.LogicUpdate();

        player.SetVelocityY(-playerData.wallSlideVelocity);

        if(grabInput && yInput ==0 && !isExitingState)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }
}
