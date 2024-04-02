using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private GameObject go;
    public PlayerAttackState(PlayerStateMachine _playerStateMachine) : base(_playerStateMachine)
    {
    }

    SkillInfoData skillInfoData;
    public override void Enter()
    {
        base.Enter();

        //stateMachine.MovementSpeedModifier = 0f;

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        
        int index = stateMachine.AttackIndex;
        stateMachine.Player.Animator.SetInteger("AttackIndex", index);

        skillInfoData = stateMachine.GetSkill();
        if (skillInfoData == null) { stateMachine.ChangeState(stateMachine.IdleState); }
        else Shoot(skillInfoData);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    private void Shoot(SkillInfoData skill)
    {
        go = stateMachine.Player.Data.AttackData.skillPool.SpawnFromPool(skill.SkillStateIndex.ToString());
        go.transform.position = stateMachine.Player.transform.position;
        go.transform.GetComponent<Skill>().data = skill;
        go.SetActive(true);
    }

    public override void Update()
    {
        base.Update();

        if(go != null && !go.activeSelf)
        {
            if (stateMachine.isCombo())
            {
                stateMachine.ChangeState(stateMachine.AttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}
