using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine 
{

    public enum GameStates { Bootstrap, MetaGame, Ingame, Pause, GameOver }
    public GameStates CurrentState { get; private set; } = GameStates.Bootstrap;

    private readonly GameplayUIService _gameplayUIService;
    private readonly MetaUiService _metaUiService;
   
    public GameStateMachine(
        MetaUiContainer metaUiContainer, 
        CoreGameplayUiContainer coreGameplayUiContainer,
        GameplayUIService gameplayUIService, 
        MetaUiService metaUiService)
    {
        Time.timeScale = 0f;
        _gameplayUIService = gameplayUIService;
        _metaUiService = metaUiService;

        //meta ui binding
        metaUiContainer.StartGameBtn.onClick.AddListener(StartGame);

        //core ui binding
        coreGameplayUiContainer.menuButton.onClick.AddListener(PauseGame);
        coreGameplayUiContainer.ContinueGameVtn.onClick.AddListener(UnpauseGame);
        coreGameplayUiContainer.ToMenu.onClick.AddListener(ExitToMenu);
    }
    private void ChangeGameState(GameStates nextState)
    {
        switch (nextState)
        {
            case GameStates.Bootstrap:
                Debug.LogWarning("Unavailable translation");
                break;

            case GameStates.MetaGame:
                CurrentState = nextState;
                break;

            case GameStates.Ingame:
                CurrentState = nextState;          
                break;

            case GameStates.Pause:
                CurrentState = nextState;
                break;

            case GameStates.GameOver:
                CurrentState = nextState;
                Time.timeScale = 0f;
                break;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        _metaUiService.DisableGui();
        _gameplayUIService.EnableGui();
        ChangeGameState(GameStates.Ingame);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        _gameplayUIService.SwitchToGameMenuLayer();        
        ChangeGameState(GameStates.Pause);
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        _gameplayUIService.SwitchToGamePlay();
        ChangeGameState(GameStates.Pause);
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
