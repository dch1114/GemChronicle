using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    private bool isJumping = false;
    public PlayerJumpState(PlayerStateMachine _playerStateMachine) : base(_playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.JumpForce = stateMachine.Player.Data.AirData.JumpForce;
        stateMachine.Player.Controller.Jump(stateMachine.JumpForce);

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if(stateMachine.Player.Controller.velocity.y < 0)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }
}
