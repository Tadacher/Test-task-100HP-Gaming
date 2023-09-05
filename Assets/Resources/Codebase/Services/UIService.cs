using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Events;
using TMPro;
using UnityEngine.SceneManagement;

public class UIService
{

    public OnBalanceChanged _balanceChangedHandlers;
    public OnDamageLevelChanged _damageLevelChangedHandlers;
    public OnRangeLevelChanged _rangeLevelChangedHandlers;
    public OnAttackspeedLevelChanged _attackspeedLevelChangedHandlers;
    public GameStateChanged _gameStateChangedHandlers;
    public OnUpgradeBought _upgradeBought;

    private UnityAction _playButtonClickHandlers, _pauseButtonClickHandlers, _restartButtonClickHandlers;
    
    private EconomicService _economicsManager;
    private GameObject _ingameLayer;
    private Button _upgradeDamage, _upgradeRange, _upgradeAttackSpeed, _tomenu;
    private TMP_Text _attackspeedLvl, _damageLvl, _rangeLvl, _balance;
    private GameObject _menuLayer;
    private Button _play, _restart;
    private GameObject _losescreenLayer;
    private Button _loseScreenRestart;

    

    public UIService(EconomicService economicScervice, CanvasRefsContainer canvasRefsContainer, GameStateMachine gameStateMachine)
    {
       
        _pauseButtonClickHandlers += gameStateMachine.PauseHandlers;
        _playButtonClickHandlers += gameStateMachine.PlayHandlers;

        _economicsManager = economicScervice;

        _ingameLayer = canvasRefsContainer.ingameLayer;
        _menuLayer = canvasRefsContainer.menuLayer;
        _losescreenLayer = canvasRefsContainer.loseScreen;
        
        _upgradeDamage = canvasRefsContainer.damageUpgrade;
        _upgradeRange = canvasRefsContainer.rangeUpgrade;
        _upgradeAttackSpeed = canvasRefsContainer.attackSpeedUpgrade;
        _tomenu = canvasRefsContainer.menuButton;

        _play = canvasRefsContainer.play;
        _restart = canvasRefsContainer.restart;

        _loseScreenRestart = canvasRefsContainer.loseScreenRestart;

        _attackspeedLvl = canvasRefsContainer.attackspeedLvl;
        _damageLvl = canvasRefsContainer.damageLvl;
        _rangeLvl = canvasRefsContainer.rangeLvl;
        _balance = canvasRefsContainer.balance;


        _balanceChangedHandlers += SetAvailability;
        _balanceChangedHandlers += DrawFunds;
        _damageLevelChangedHandlers += DrawDamageLevel;
        _rangeLevelChangedHandlers += DrawRangeLevel;
        _attackspeedLevelChangedHandlers += DrawAttackSpeedLevel;
        _gameStateChangedHandlers += SetActiveLayer;
        _restartButtonClickHandlers += () => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); };
        
        _upgradeDamage.onClick.AddListener(() => _economicsManager.BuyUpgrade(UpgradeService.UpgradeType.Damage));
        _upgradeRange.onClick.AddListener(() => _economicsManager.BuyUpgrade(UpgradeService.UpgradeType.Range));
        _upgradeAttackSpeed.onClick.AddListener(() => _economicsManager.BuyUpgrade(UpgradeService.UpgradeType.AttackSpeed));
        _tomenu.onClick.AddListener(_pauseButtonClickHandlers);

        _play.onClick.AddListener(_playButtonClickHandlers);
        _restart.onClick.AddListener(_restartButtonClickHandlers);

        _loseScreenRestart.onClick.AddListener(_restartButtonClickHandlers);

    }

    private void SetAvailability(int balance)
    {
        _upgradeAttackSpeed.interactable = _economicsManager.CheckForAvailability(UpgradeService.UpgradeType.AttackSpeed);
        _upgradeRange.interactable = _economicsManager.CheckForAvailability(UpgradeService.UpgradeType.Range);
        _upgradeDamage.interactable = _economicsManager.CheckForAvailability(UpgradeService.UpgradeType.Damage);
    }

    private void DrawDamageLevel(int level) => 
        _damageLvl.text = level.ToString();

    private void DrawRangeLevel(int level) => 
        _rangeLvl.text = level.ToString();

    private void DrawAttackSpeedLevel(int level) => 
        _attackspeedLvl.text = level.ToString();

    private void DrawFunds(int funds) => 
        _balance.text = funds.ToString() + " $";

    private void TranslateToPlay()
    {
        _ingameLayer.SetActive(true);
        _menuLayer.SetActive(false);
    }

    private void TranslateToPause()
    {
        _ingameLayer.SetActive(false);
        _menuLayer.SetActive(true);
    }

    private void TranslateToLoseScreen()
    {
        _ingameLayer.SetActive(false);
        _losescreenLayer.SetActive(true);
    }

    private void SetActiveLayer(GameStateMachine.GameStates nextState)
    {
        switch (nextState)
        {
            case GameStateMachine.GameStates.GameOver:
                TranslateToLoseScreen();
                break;
            case GameStateMachine.GameStates.Start:
                
                break;
            case GameStateMachine.GameStates.Ingame:
                TranslateToPlay();
                break;
            case GameStateMachine.GameStates.Pause:
                TranslateToPause();
                break;
        }
    }
}
