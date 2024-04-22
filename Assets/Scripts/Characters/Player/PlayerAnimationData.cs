using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string attackIndexParameterName = "AttackIndex";
    [SerializeField] private string dieParameterName = "Die";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }

    public int AttackParameterHash { get; private set;}
    public int AttackIndexParameterHash { get; private set; }

    public int DieParameterHash { get; private set; }
    //public int Skill1IndexParameterHash { get; private set; }
    //public int Skill2IndexParameterHash { get; private set; }
    //public int Skill3IndexParameterHash { get; private set; }
    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);

        AirParameterHash = Animator.StringToHash(airParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        FallParameterHash = Animator.StringToHash(fallParameterName);

        AttackParameterHash = Animator.StringToHash(attackParameterName);
        AttackIndexParameterHash = Animator.StringToHash(attackIndexParameterName);

        DieParameterHash = Animator.StringToHash(dieParameterName);
        //Skill1IndexParameterHash = Animator.StringToHash(skill1IndexParameterName);
        //Skill2IndexParameterHash = Animator.StringToHash(skill2IndexParameterName);
        //Skill3IndexParameterHash = Animator.StringToHash(skill3IndexParameterName);
    }
}
