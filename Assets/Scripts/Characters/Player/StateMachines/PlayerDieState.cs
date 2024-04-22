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
        //UIManager�� ��� panel �ѱ�
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.DieParameterHash);
    }
}
