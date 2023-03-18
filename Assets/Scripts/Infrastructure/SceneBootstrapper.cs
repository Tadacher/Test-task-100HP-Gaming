using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBootstrapper : MonoBehaviour
{
    [SerializeField] GlobalSettings globalSettings;

    [SerializeField] GameObject playerBasePrefab;
    PlayerBaseBehaviour playerBase;

    [SerializeField] GameObject asteroidFactoryPrefab;
    AsteroidFactory asteroidFactory;

    [SerializeField] GameObject audioManagerPrefab;
    AudioManager audioManager;

    [SerializeField] GameObject missileFactoryPrefab;
    MissileFactory missileFactory;

    [SerializeField] GameObject economicsManagerPrefab;
    EconomicsManager economicsManager;

    [SerializeField] GameObject upgradeManagerPrefab;
    UpgradeManager upgradeManager;

    [SerializeField] GameObject uImanagerPrefab;
    UImanager uImanager;

    [SerializeField] GameObject canvasRefsContainerPrefab;
    CanvasRefsContainer canvasRefsContainer;

    [SerializeField] GameObject lineRendererPrefab;
    LineRendererScript lineRenderer;

    [SerializeField] GameObject gameStateMachinePrefab;
    GameStateMachine gameStateMachine;

    [SerializeField] GameObject gfxManagerPrefab;
    GfxManager gfxManager;

    private void Awake()
    {
        BindPhase();
        InitPhase();
    }
    void BindPhase()
    {
        playerBase = Instantiate(playerBasePrefab, null).GetComponent<PlayerBaseBehaviour>();
        asteroidFactory = Instantiate(asteroidFactoryPrefab, transform).GetComponent<AsteroidFactory>();
        economicsManager = Instantiate(economicsManagerPrefab, transform).GetComponent<EconomicsManager>();
        missileFactory = Instantiate(missileFactoryPrefab, transform).GetComponent<MissileFactory>();
        upgradeManager = Instantiate(upgradeManagerPrefab, transform).GetComponent<UpgradeManager>();
        uImanager = Instantiate(uImanagerPrefab, transform).GetComponent<UImanager>();
        canvasRefsContainer = Instantiate(canvasRefsContainerPrefab, null).GetComponent<CanvasRefsContainer>();
        lineRenderer = Instantiate(lineRendererPrefab, null).GetComponent<LineRendererScript>();
        audioManager = Instantiate(audioManagerPrefab, transform).GetComponent<AudioManager>();
        gameStateMachine = Instantiate(gameStateMachinePrefab, transform).GetComponent<GameStateMachine>();
        gfxManager = Instantiate(gfxManagerPrefab, transform).GetComponent<GfxManager>();
    }
    void InitPhase()
    {
        InitAsteroidFactory();
        InitPlayerBase();
        InitEconomicManager();
        InitUpgradeManager();
        InitLineRenderer();
        InitAudioManager();
        InitGameStateMachine();
        InitGfxManager();
        InitUiManager();
        InitMissileFactory();
    }
    void InitAsteroidFactory()
    {
        asteroidFactory.Initialize(globalSettings.asteroidSettings);
        asteroidFactory.Target = playerBase.transform;     
    }
    void InitPlayerBase()
    {
        playerBase.Initialize(globalSettings.playerBehaviourSettings);
        playerBase.MissileFactory = missileFactory;
    }
    void InitEconomicManager()
    {
        economicsManager.Initialize(upgradeManager, globalSettings.economicsManagerSettings);
        asteroidFactory.paidAsteroidDeathHandlers += economicsManager.CountIncome;
    }
    void InitUpgradeManager()
    {
        upgradeManager.Initialize(missileFactory, playerBase, globalSettings.upgraderSettings);
    }
    void InitUiManager()
    {
        uImanager.Initialize(economicsManager, canvasRefsContainer, upgradeManager);
        
        gameStateMachine.OnGameStateChanged += uImanager.gameStateChangedHandlers;
        
        economicsManager.OnBalanceChanged += uImanager.balanceChangedHandlers;
        
        upgradeManager.OndamageLevelChanged += uImanager.damageLevelChangedHandlers;
        upgradeManager.OnRangeLevelChanged += uImanager.rangeLevelChangedHandlers;
        upgradeManager.OnAttackspeedLevelChanged += uImanager.attackspeedLevelChangedHandlers;
    }
    void InitLineRenderer()
    {
        lineRenderer.Initialize();
        lineRenderer.rangeLevelChangedHandlers.Invoke(upgradeManager.GetRange());
        upgradeManager.OnrangeChanged += lineRenderer.rangeLevelChangedHandlers;
    }
    void InitAudioManager()
    {
        audioManager.Initialize();
        missileFactory.OnMissileLaunched += audioManager.missileLaunchedSoundHandlers;
        asteroidFactory.paidAsteroidDeathHandlers += audioManager.asteroidDeathSoundHandlers;
        economicsManager.OnUpgradeBought += audioManager.upgradeBoughtSoundHandlers;
    }
    void InitGameStateMachine()
    {
        gameStateMachine.Initialize();
        
        playerBase.OnPlayerDeath += gameStateMachine.playerDeathHandlers;
        
        uImanager.pauseButtonClickHandlers += gameStateMachine.PauseHandlers;

        
        uImanager.playButtonClickHandlers += gameStateMachine.PlayHandlers;
        Debug.Log(uImanager.playButtonClickHandlers);

    }
    void InitGfxManager()
    {
        gfxManager.Initialize();
        missileFactory.missileDestroyedHandlers += gfxManager.missileDestroyedGfxHandlers;
    }
    void InitMissileFactory()
    {
        missileFactory.Initialize(globalSettings.missilefactorySettings);
    }
}
