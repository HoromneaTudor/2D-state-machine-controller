using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.JumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();

        //the tranzition is instantaneous and because we dont tell JumpState to do something else with LogicUpdate, then it will do the base.LogicUpdate
        //Important!! un singur Logic Update poate fi apelat o singura data, fie cel din parinte sau copil (base sau cel implicit)
        //deci nu putem avea doua logic Update o data, putem avea sa faca acelasi lucru suprascriind logic updateul spre exemplu aici
        //doar ca atunci vom prelua ce face celalalt, celalalt fiind oprit dar avand acelasi efect din cauza ca a fost suprascris (base.LogicUpdate)
        //daca dorim putem scoare base.logicUpdate si sa il schimbam dar nu ast a este ideia in acest state machine
        //cel putin asa cred si asa ar trebui
        
    }


    public bool CanJump()
    {
        if(amountOfJumpsLeft>0)
        {
            return true;
        }
        return false;
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
