using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    public CinemachineConfiner2D cine2d;


    public Player player;

    public bool isNew = true;
    public string playerName { get; set; } = "±‚∫ª¿Ã";
    public JobType playerJob { get; set; } = JobType.Warrior;
    public bool isLastBossDead = false;
    public int saveDataID;

    public Inventory inventory;
    protected override void Awake()
    {
        base.Awake();

        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            DontDestroyOnLoad(Instance);
        }
    }

}
