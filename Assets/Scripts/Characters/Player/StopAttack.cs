using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAttack : MonoBehaviour
{
    public Player player;

    public void StopAttacking()
    {
        PlayerStateMachine stateMachine = player.GetStateMachine();

        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
