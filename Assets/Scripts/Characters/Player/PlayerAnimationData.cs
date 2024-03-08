using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string skill1ParameterName = "Skill1";
    [SerializeField] private string skill2ParameterName = "Skill2";
    [SerializeField] private string skill3ParameterName = "Skill3";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }

    public int AttackParameterHash { get; private set;}
    public int Skill1ParameterHash { get; private set; }
    public int Skill2ParameterHash { get; private set; }
    public int Skill3ParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        AirParameterHash = Animator.StringToHash(airParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        FallParameterHash = Animator.StringToHash(fallParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        Skill1ParameterHash = Animator.StringToHash(skill1ParameterName);
        Skill2ParameterHash = Animator.StringToHash(skill2ParameterName);
        Skill3ParameterHash = Animator.StringToHash(skill3ParameterName);
    }
}
