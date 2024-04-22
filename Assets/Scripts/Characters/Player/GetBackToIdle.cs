using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBackToIdle : MonoBehaviour
{
    public Player player;

    public void ToIdleState()
    {
        PlayerStateMachine stateMachine = player.GetStateMachine();

        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
