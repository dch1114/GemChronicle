using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerBaseState
{
    public PlayerDieState(PlayerStateMachine _playerStateMachine) : base(_playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.DieParameterHash);
        //UIManagerÀÇ »ç¸Á panel ÄÑ±â
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.DieParameterHash);
    }
}
