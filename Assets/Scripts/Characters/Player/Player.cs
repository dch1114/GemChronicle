using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerDatas Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerController Controller { get; private set; }

    private PlayerStateMachine stateMachine;

    public GameObject SkillPage { get; set; }
    [HideInInspector] public SkillPagesUI skillpageUI; //test

    //test
    public GameObject InventoryUIPanel;
    [HideInInspector] public InventoryUIController inventoryUIController;

    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<PlayerController>();

        stateMachine = new PlayerStateMachine(this);
        //test
        inventoryUIController = InventoryUIPanel.GetComponentInParent<InventoryUIController>();

    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);
        skillpageUI = UIManager.Instance.skillPages;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public PlayerStateMachine GetStateMachine()
    {
        return stateMachine;
    }
}
