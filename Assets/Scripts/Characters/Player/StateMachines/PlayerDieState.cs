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
        stateMachine.Player.Animator.SetTrigger(stateMachine.Player.AnimationData.DieParameterHash);

        if (UIManager.Instance != null) UIManager.Instance.diePanelUI.ActiveDiePanel();
    }

    public override void Exit()
    {
        base.Exit();
        //stateMachine.Player.Animator.SetTrigger(stateMachine.Player.AnimationData.DieParameterHash);
    }
}
