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
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext _context)
    {

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

        //normalize 필요 없을듯?
        //right.Normalize();

        return right * stateMachine.MovementInput.x;
    }
    private void Move(Vector3 _movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Player.Controller.Move(
            (_movementDirection * movementSpeed) * Time.deltaTime);
    }

    private void Look(Vector3 _movementDirection)
    {
        //이동 방향
        if(_movementDirection.x < 0f)
        {
            stateMachine.Player.Controller.Look(true);
        } else if(_movementDirection.x > 0f)
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
