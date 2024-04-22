using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }

    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }
    public PlayerAttackState AttackState { get; }

    public PlayerDieState DieState { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }

    public bool IsAttacking { get; set; }
    public bool IsComboAttacking { get; set; }
    public int AttackIndex { get; set; }

    public List<int> SkillIndex { get; set; }
    public List<SkillInfoData> skillInfoDatas { get; set; } = new List<SkillInfoData>();
    public void SetUseSkill(int index)
    {
        SkillIndex = Player.Data.AttackData.AttackSkillStates[index];

        int cnt = (Player.Data.StatusData.JobType == JobType.Warrior) ? SkillIndex.Count : 1;
        
        for(int i = 0; i < cnt; i++)
        {
            SkillInfoData s = Player.Data.AttackData.GetSkillInfo(SkillIndex[i]);
            skillInfoDatas.Add(s);
        }
    }

    public SkillInfoData GetSkill()
    {
        if (skillInfoDatas == null) return null;
        if (skillInfoDatas.Count == 0) return null;

        SkillInfoData skillInfoData = skillInfoDatas[0];
        skillInfoDatas.Remove(skillInfoData);

        return skillInfoData;
    }

    public bool isCombo()
    {
        return skillInfoDatas.Count > 0;
    }
    public void ResetSkillInfos()
    {
        skillInfoDatas.Clear();
    }

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player _player)
    {
        this.Player = _player;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);

        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);

        AttackState = new PlayerAttackState(this);

        DieState = new PlayerDieState(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = _player.Data.GroundedData.BaseSpeed;
        JumpForce = _player.Data.AirData.JumpForce;
    }
}