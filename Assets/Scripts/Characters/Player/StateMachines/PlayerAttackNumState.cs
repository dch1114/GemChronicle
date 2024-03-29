using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
        Shoot(skillIndex);

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Shoot(List<int> skills)
    {
        List<GameObject> skillObjects = new List<GameObject>();
        for(int i = 0; i < skills.Count; i++)
        {
            GameObject go = stateMachine.Player.Data.AttackData.skillPool.SpawnFromPool(skills[i].ToString());
            skillObjects.Add(go);
        }

        SkillManager.Instance.SkillCombo(skillObjects);
    }
}
