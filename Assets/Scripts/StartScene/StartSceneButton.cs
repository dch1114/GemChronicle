using UnityEngine;
using UnityEngine.UI;

public class StartSceneButton : MonoBehaviour
{
    public Button newGame;
    public Button createCharacter;
    public Button turnBack;

    StartManager startManager;

    // Start is called before the first frame update
    void Start()
    {
        startManager = GetComponent<StartManager>();

        newGame.onClick.AddListener(OnClickNewGameButton);
        createCharacter.onClick.AddListener(OnClickCreateCharacter);
        turnBack.onClick.AddListener(OnClickTurnBackButton);
    }



    // Update is called once per frame
    void Update()
    {

    }

    void OnClickNewGameButton()
    {
        startManager.CharacterChoosPrefab.SetActive(true);
        startManager.gameStart.SetActive(false);
        startManager.gameTitle.gameObject.SetActive(false);
    }

    void OnClickTurnBackButton()
    {
        startManager.gameTitle.gameObject.SetActive(true);
        startManager.gameStart.SetActive(true);
        startManager.CharacterChoosPrefab.SetActive(false);
    }

    private void OnClickCreateCharacter()
    {
        startManager.StartNewGame();
    }

}
