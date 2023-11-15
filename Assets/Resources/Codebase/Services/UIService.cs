using UnityEngine.SceneManagement;

public class UIService
{
    private CanvasRefsContainer _canvas;
    private GameStateMachine _gameStateMachine;
    public UIService(CanvasRefsContainer canvasRefsContainer, GameStateMachine gameStateMachine)
    {
        _canvas = canvasRefsContainer;
        _gameStateMachine = gameStateMachine;
       
        _canvas.menuButton.onClick.AddListener(PauseGame);
        _canvas.play.onClick.AddListener(StartGame);
        _canvas.restart.onClick.AddListener(() => SceneManager.LoadScene(0));
        _canvas.loseScreenRestart.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

    private void PauseGame()
    {
        _gameStateMachine.PauseGame();
        TranslateToPause();
    }

    private void StartGame()
    {
        _gameStateMachine.StartGame();
        TranslateToPlay();
    }
    public void SetDamageUpgradeAvailability(bool state)
        => _canvas.damageUpgrade.interactable = state;
    public void SetRangeUpgradeAvailability(bool state)
        => _canvas.rangeUpgrade.interactable = state;
    public void SetAttackSpeedUpgradeAvailability(bool state) 
        => _canvas.attackSpeedUpgrade.interactable = state;

    public void DrawDamageLevel(int level) =>
        _canvas.damageLvl.text = level.ToString();

    public void DrawRangeLevel(int level) =>
        _canvas.rangeLvl.text = level.ToString();

    public void DrawAttackSpeedLevel(int level) =>
       _canvas.attackspeedLvl.text = level.ToString();

    public void DrawFunds(int funds) =>
       _canvas.balance.text = funds.ToString() + " $";

    private void TranslateToPlay()
    {
        _canvas.ingameLayer.SetActive(true);
        _canvas.menuLayer.SetActive(false);
    }

    private void TranslateToPause()
    {
        _canvas.ingameLayer.SetActive(false);
        _canvas.menuLayer.SetActive(true);
        _gameStateMachine.PauseGame();
    }

    private void TranslateToLoseScreen()
    {
        _canvas.ingameLayer.SetActive(false);
        _canvas.loseScreen.SetActive(true);
    }
}
