using UnityEngine;

public class HideNpc : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Unit;
    [SerializeField] private CircleCollider2D collider1;
    [SerializeField] private CircleCollider2D collider2;

    public void Start()
    {

    }
    void Update()
    {
        if (QuestManager.Instance != null)
        {
            if (QuestManager.Instance.hideNPC == true)
            {
                Unit.SetActive(false);
                collider1.enabled = false;
                collider2.enabled = false;

            }
            if (QuestManager.Instance.hideNPC == false)
            {
                Unit.SetActive(true);
                collider1.enabled = true;
                collider2.enabled = true;
            }
        }
    }
}
