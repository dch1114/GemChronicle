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
    public PlayerAttackNumState AttackNumState { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }

    public bool IsAttacking { get; set; }
    public bool IsComboAttacking { get; set; }
    public int AttackIndex { get; set; }
    public List<int> SkillIndex { get; set; }

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player _player)
    {
        this.Player = _player;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);

        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);

        AttackNumState = new PlayerAttackNumState(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = _player.Data.GroundedData.BaseSpeed;
        JumpForce = _player.Data.AirData.JumpForce;
    }
}