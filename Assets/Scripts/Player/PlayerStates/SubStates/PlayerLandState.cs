using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if(xInput !=0 && !JumpInput) //am facut jumpInputul protected pt ca aveam nevoid ca sa stiu daca atunci cand aterizez vreau sa sar din nou imediat
                                        //sa nu ma duca in movestate pt ca dupa voi avea un al doilea jump(ceea ce nu imi doresc acum)
        
        if(!isExitingState)
        {
            if (xInput != 0)                //Original acum merge si asta, nust ce nu ii placea
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
