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



        input.PlayerActions.SkillPage.started += OnSkillPageStarted;

        input.PlayerActions.AttackA.performed += OnAttackAPerformed;
        input.PlayerActions.AttackA.canceled += OnAttackCanceled;
        input.PlayerActions.AttackS.performed += OnAttackSPerformed;
        input.PlayerActions.AttackS.canceled += OnAttackCanceled;
        input.PlayerActions.AttackD.performed += OnAttackDPerformed;
        input.PlayerActions.AttackD.canceled += OnAttackCanceled;

        //Test
        input.PlayerActions.Inventory.started += OnInventory;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;

        

        input.PlayerActions.SkillPage.started -= OnSkillPageStarted;

        input.PlayerActions.AttackA.performed -= OnAttackAPerformed;
        input.PlayerActions.AttackA.canceled -= OnAttackCanceled;
        input.PlayerActions.AttackS.performed -= OnAttackSPerformed;
        input.PlayerActions.AttackS.canceled -= OnAttackCanceled;
        input.PlayerActions.AttackD.performed -= OnAttackDPerformed;
        input.PlayerActions.AttackD.canceled -= OnAttackCanceled;

        //Test
        input.PlayerActions.Inventory.started -= OnInventory;
    }


    protected virtual void OnAttackAPerformed(InputAction.CallbackContext context)
    {
        SetAttackIndexs(0);
    }

    protected virtual void OnAttackSPerformed(InputAction.CallbackContext context)
    {
        SetAttackIndexs(1);
    }

    protected virtual void OnAttackDPerformed(InputAction.CallbackContext context)
    {
        SetAttackIndexs(2);
    }

    private void SetAttackIndexs(int _index)
    {
        stateMachine.AttackIndex = _index;
        stateMachine.ResetSkillInfos();
        stateMachine.SetUseSkill(_index);
        stateMachine.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = false;
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
        bool isActive = stateMachine.Player.InventoryUIPrefab.activeSelf;
        stateMachine.Player.InventoryUIPrefab.SetActive(!isActive);
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
        Vector3 right = stateMachine.MainCameraTransform.right;
        right.y = 0;
        right.Normalize();

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
            stateMachine.Player.Controller.isLeft = true;
        } else if (_movementDirection.x > 0f)
        {
            stateMachine.Player.Controller.isLeft = false;
        }

        stateMachine.Player.Controller.Look();
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
