using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine _playerStateMachine) : base(_playerStateMachine)
    {
    }

    public override void Enter()
    {
        //stateMachine.MovementSpeedModifier = 0f;
        base.Enter();

        int index = stateMachine.AttackIndex;
        stateMachine.Player.Animator.SetInteger("AttackIndex", index);

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        List<int> skillIndex = stateMachine.SkillIndex;
        Shoot(skillIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    private void Shoot(List<int> skills)
    {
        List<GameObject> skillObjects = new List<GameObject>();
        for (int i = 0; i < skills.Count; i++)
        {
            GameObject go = stateMachine.Player.Data.AttackData.skillPool.SpawnFromPool(skills[i].ToString());
            skillObjects.Add(go);
        }

        SkillManager.Instance.SkillCombo(skillObjects);
    }
}
