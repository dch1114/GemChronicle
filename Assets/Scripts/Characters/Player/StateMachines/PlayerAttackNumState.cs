using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackNumState : PlayerAttackState
{
    public PlayerAttackNumState(PlayerStateMachine _playerStateMachine) : base(_playerStateMachine)
    {
    }

    public override void Enter()
    {
        int index = stateMachine.AttackIndex;
        stateMachine.Player.Animator.SetInteger("AttackIndex", index);

        //TODO: Skill 찍는거 만들어서 반영
        List<int> skillIndex = stateMachine.SkillIndex;
        stateMachine.Player.Animator.SetInteger("Skill1Index", skillIndex[0]);
        stateMachine.Player.Animator.SetInteger("Skill2Index", skillIndex[1]);
        stateMachine.Player.Animator.SetInteger("Skill3Index", skillIndex[2]);

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
