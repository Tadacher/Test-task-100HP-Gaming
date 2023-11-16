using System;
using UnityEngine.SceneManagement;

public class GameplayUIService
{
    private CoreGameplayUiContainer _container;
    public GameplayUIService(CoreGameplayUiContainer canvasRefsContainer)
    {
        _container = canvasRefsContainer;
       
        //_canvas.play.onClick.AddListener(StartGame);
        //_canvas.restart.onClick.AddListener(() => SceneManager.LoadScene(0));
        _container.LoseScreenToMenu.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

   
    public void EnableGui()
       => _container.gameObject.SetActive(true);
    public void DisableGui()
        => _container.gameObject.SetActive(false);

    public void SetDamageUpgradeAvailability(bool state)
        => _container.damageUpgrade.interactable = state;
    public void SetRangeUpgradeAvailability(bool state)
        => _container.rangeUpgrade.interactable = state;
    public void SetAttackSpeedUpgradeAvailability(bool state) 
        => _container.attackSpeedUpgrade.interactable = state;

    public void DrawDamageLevel(int level) =>
        _container.damageLvl.text = level.ToString();

    public void DrawRangeLevel(int level) =>
        _container.rangeLvl.text = level.ToString();

    public void DrawAttackSpeedLevel(int level) =>
       _container.attackspeedLvl.text = level.ToString();

    public void DrawFunds(int funds) =>
       _container.balance.text = funds.ToString() + " $";

    public void SwitchToGamePlay()
    {
        _container.GameMenuLayer.SetActive(false);
        _container.GameplayUiLayer.SetActive(true);
    }
    public void SwitchToLoseScreen()
    {
        _container.GameplayUiLayer.SetActive(false);
        _container.LoseScreen.SetActive(true);
    }

    public void SwitchToGameMenuLayer()
    {
        _container.GameMenuLayer.SetActive(true);
        _container.GameplayUiLayer.SetActive(false);
    }

}
