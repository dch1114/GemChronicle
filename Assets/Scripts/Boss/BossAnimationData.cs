using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string attackIndexParameterName = "AttackIndex";
    [SerializeField] private string dieParameterName = "Die";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int AttackIndexParameterHash { get; private set; }

    public int DieParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);

        AttackParameterHash = Animator.StringToHash(attackParameterName);
        AttackIndexParameterHash = Animator.StringToHash(attackIndexParameterName);

        DieParameterHash = Animator.StringToHash(dieParameterName);
    }
}