using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine _playerStateMachine)
    {
        stateMachine = _playerStateMachine;
        groundData = stateMachine.Player.Data.GroundedData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void Update()
    {
        Move();
    }

    public virtual void PhysicsUpdate()
    {

    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;

        input.PlayerActions.Jump.started += OnJumpStarted;

        input.PlayerActions.SkillPage.started += OnSkillPageStarted;

        input.PlayerActions.ComboAttack.started += OnComboAttackStarted;

        input.PlayerActions.Attack.performed += OnAttackPerformed;
        input.PlayerActions.Attack.canceled += OnAttackCanceled;

        //Test
        input.PlayerActions.Inventory.started += OnInventory;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;

        stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;

        input.PlayerActions.SkillPage.started -= OnSkillPageStarted;

        input.PlayerActions.Attack.performed -= OnAttackPerformed;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;

        input.PlayerActions.ComboAttack.canceled += OnComboAttackCanceled;

        //Test
        input.PlayerActions.Inventory.started -= OnInventory;
    }


    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if(!stateMachine.IsComboAttacking)
        {
            stateMachine.IsAttacking = true;

            //TODO: Skill Index 연동 필요
            InputAction action = context.action;

            if (action.activeControl.Equals(action.controls[0]))
            {
                stateMachine.AttackIndex = 1;
                stateMachine.SkillIndex = GetSkillNumIndexs(0);
            }
            else if (action.activeControl.Equals(action.controls[1]))
            {
                stateMachine.AttackIndex = 2;
                stateMachine.SkillIndex = GetSkillNumIndexs(1);
            }
            else if (action.activeControl.Equals(action.controls[2]))
            {
                stateMachine.AttackIndex = 3;
                stateMachine.SkillIndex = GetSkillNumIndexs(2);
            }
        }
    }

    private List<int> GetSkillNumIndexs(int index)
    {
        switch(stateMachine.Player.Data.StatusData.JobType)
        {
            case JobType.Warrior:
                return stateMachine.Player.Data.AttackData.AttackSkillStates[index];
                break;
            default:
                return stateMachine.Player.Data.AttackData.AttackSkillStates[0];
                break;
        }
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = false;
    }

    protected virtual void OnComboAttackStarted(InputAction.CallbackContext context)
    {
        stateMachine.IsComboAttacking = true;
    }
    protected virtual void OnComboAttackCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsComboAttacking = false;
    }

    private void OnSkillPageStarted(InputAction.CallbackContext context)
    {
        bool isActive = stateMachine.Player.SkillPage.activeSelf;
        stateMachine.Player.SkillPage.SetActive(!isActive);
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext _context)
    {

    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }

    //test
    private void OnInventory(InputAction.CallbackContext context)
    {
        bool isActive = stateMachine.Player.InventoryPanel.activeSelf;
        stateMachine.Player.InventoryPanel.SetActive(!isActive);
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Move(movementDirection);
        Look(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 right = new Vector3(1f, 0f, 0f);
        //right.Normalize();

        return right * stateMachine.MovementInput.x;
    }
    private void Move(Vector3 _movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Player.Controller.Move(
            ((_movementDirection * movementSpeed) + stateMachine.Player.Controller.Movement) * Time.deltaTime);
    }

    private void Look(Vector3 _movementDirection)
    {
        //이동 방향
        if (_movementDirection.x < 0f)
        {
            stateMachine.Player.Controller.Look(true);
        } else if (_movementDirection.x > 0f)
        {
            stateMachine.Player.Controller.Look(false);
        }
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;

        return movementSpeed;
    }

    protected void StartAnimation(int _animationHash)
    {
        stateMachine.Player.Animator.SetBool(_animationHash, true);
    }

    protected void StopAnimation(int _animationHash)
    {
        stateMachine.Player.Animator.SetBool(_animationHash, false);
    }
}
