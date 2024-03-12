using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine _playerStateMachine) : base(_playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("FallENter");

        StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Exit() 
    { 
        base.Exit();

        Debug.Log("FallExit");
        StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Player.Controller.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
