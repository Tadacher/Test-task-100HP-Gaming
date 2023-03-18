using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Events;
using TMPro;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    EconomicsManager economicsManager;
    EconomicsManager EconomicsManager
    {
        get { return economicsManager; }
        set 
        {
            if (economicsManager == null) economicsManager = value;
            else ConsoleShortCuts.FieldOverrideWarn("economicsManager");
        }
    }
    UpgradeManager upgradeManager;
    UpgradeManager UpgradeManager
    {
        get { return upgradeManager; }
        set
        {
            if (upgradeManager == null) upgradeManager = value;
            else ConsoleShortCuts.FieldOverrideWarn("upgradeManager");
        }
    }
    GameObject ingameLayer;
    Button upgradeDamage, upgradeRange, upgradeAttackSpeed, tomenu;
    TMP_Text attackspeedLvl, damageLvl, rangeLvl, balance;

    GameObject menuLayer;
    Button play, restart;

    GameObject losescreenLayer;
    Button loseScreenRestart;

    
    internal UnityAction playButtonClickHandlers, pauseButtonClickHandlers, restartButtonClickHandlers;

    internal BalanceChanged balanceChangedHandlers;
    internal DamageLevelChanged damageLevelChangedHandlers;
    internal RangeLevelChanged rangeLevelChangedHandlers;
    internal AttackspeedLevelChanged attackspeedLevelChangedHandlers;
    internal GameStateChanged gameStateChangedHandlers;
    internal UpgradeBought upgradeBought;

    internal void Initialize(EconomicsManager _economicsManager, CanvasRefsContainer _canvasRefsContainer, UpgradeManager _upgradeManager)
    {
        if (_economicsManager == null) ConsoleShortCuts.NotInjectedWarn("UImanager");
        if (_canvasRefsContainer == null) ConsoleShortCuts.NotInjectedWarn("CanvasRefsContainer");
        if (_upgradeManager == null) ConsoleShortCuts.NotInjectedWarn("UpgradeManager");

        economicsManager = _economicsManager;
        upgradeManager = _upgradeManager;

        ingameLayer = _canvasRefsContainer.ingameLayer;
        menuLayer = _canvasRefsContainer.menuLayer;
        losescreenLayer = _canvasRefsContainer.loseScreen;
        
        upgradeDamage = _canvasRefsContainer.damageUpgrade;
        upgradeRange = _canvasRefsContainer.rangeUpgrade;
        upgradeAttackSpeed = _canvasRefsContainer.attackSpeedUpgrade;
        tomenu = _canvasRefsContainer.menuButton;

        play = _canvasRefsContainer.play;
        restart = _canvasRefsContainer.restart;

        loseScreenRestart = _canvasRefsContainer.loseScreenRestart;

        attackspeedLvl = _canvasRefsContainer.attackspeedLvl;
        damageLvl = _canvasRefsContainer.damageLvl;
        rangeLvl = _canvasRefsContainer.rangeLvl;
        balance = _canvasRefsContainer.balance;


        balanceChangedHandlers += SetAvailability;
        balanceChangedHandlers += DrawFunds;
        damageLevelChangedHandlers += DrawDamageLevel;
        rangeLevelChangedHandlers += DrawRangeLevel;
        attackspeedLevelChangedHandlers += DrawAttackSpeedLevel;
        gameStateChangedHandlers += SetActiveLayer;
        restartButtonClickHandlers += () => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); };
        
        upgradeDamage.onClick.AddListener(() => { economicsManager.BuyUpgrade(UpgradeManager.UpgradeType.Damage); });
        upgradeRange.onClick.AddListener(() => { economicsManager.BuyUpgrade(UpgradeManager.UpgradeType.Range); });
        upgradeAttackSpeed.onClick.AddListener(() => { economicsManager.BuyUpgrade(UpgradeManager.UpgradeType.AttackSpeed); });
        tomenu.onClick.AddListener(pauseButtonClickHandlers);

        play.onClick.AddListener(playButtonClickHandlers);
        Debug.Log(play.onClick.ToString());
        restart.onClick.AddListener(restartButtonClickHandlers);

        loseScreenRestart.onClick.AddListener(restartButtonClickHandlers);

    }
    void SetAvailability(int balance)
    {
        upgradeAttackSpeed.interactable = economicsManager.CheckForAvailability(UpgradeManager.UpgradeType.AttackSpeed);
        upgradeRange.interactable = economicsManager.CheckForAvailability(UpgradeManager.UpgradeType.Range);
        upgradeDamage.interactable = economicsManager.CheckForAvailability(UpgradeManager.UpgradeType.Damage);
    }
    void DrawDamageLevel(int level)
    {
        damageLvl.text = level.ToString();
    }
    void DrawRangeLevel(int level)
    {
        rangeLvl.text = level.ToString();
    }
    void DrawAttackSpeedLevel(int level)
    {
        attackspeedLvl.text = level.ToString();
    }
    void DrawFunds(int funds)
    {
        balance.text = funds.ToString() + " $";
    }
    void TranslateToPlay()
    {
        ingameLayer.SetActive(true);
        menuLayer.SetActive(false);
    }
    void TranslateToPause()
    {
        ingameLayer.SetActive(false);
        menuLayer.SetActive(true);
    }
    void TranslateToLoseScreen()
    {
        ingameLayer.SetActive(false);
        losescreenLayer.SetActive(true);
    }
    

    void SetActiveLayer(GameStateMachine.GameStates nextState)
    {
        Debug.Log("ImmaCalled");
        Debug.Log(nextState);
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
