using UnityEngine;

public class SceneMonoInstaller : AbstractMonoInstaller
{
    [SerializeField] private GlobalSettings _globalSettings;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private PlayerBaseBehaviour _playerBase;
    [SerializeField] private LineRendererScript _lineRenderer;

    [SerializeField] private CoreGameplayUiContainer _gameplayCanvas;
    [SerializeField] private MetaUiContainer _metaUiContainer;

    public override void InstallBindings()
    {
        BindUnityComponentFromInstance(_audioSource);

        BindUnityComponentFromInstance(_gameplayCanvas);
        BindUnityComponentFromInstance(_metaUiContainer);


        BindMonobehaviourService(_coroutineRunner);
        BindMonobehaviourService(_lineRenderer);
        BindMonobehaviourService(_playerBase);

        BindScriptableObject(_globalSettings.asteroidSettings);
        BindScriptableObject(_globalSettings.audioSetup);
        BindScriptableObject(_globalSettings.upgraderSettings);
        BindScriptableObject(_globalSettings.playerBehaviourSettings);
        BindScriptableObject(_globalSettings.missilefactorySettings);
        BindScriptableObject(_globalSettings.economicsManagerSettings);

        //misc
        BindService<AudioService>();
        BindService<UpgradeService>();
        BindService<EconomicService>();

        //factories
        BindService<MissileFactory>();
        BindService<AsteroidFactory>();
        BindService<ParticleFactory>();
        //GUI
        BindService<GameplayUIService>();
        BindService<MetaUiService>();
        //Infrastructure
        BindService<GameStateMachine>();
        BindService<GameLoopService>();


       // PostinitPhase();
    }

    private void PostinitPhase()
    {
        _lineRenderer.RedrawCircle(_globalSettings.upgraderSettings.initialRange);
        Container.QueueForInject(_playerBase);
    }
}
