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
        Shoot(skillIndex[0]);

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Shoot(int _index)
    {
        GameObject go = stateMachine.Player.Data.AttackData.skillPool.SpawnFromPool(_index.ToString());
        go.transform.position = stateMachine.Player.transform.position;
        go.SetActive(true);
    }
}
