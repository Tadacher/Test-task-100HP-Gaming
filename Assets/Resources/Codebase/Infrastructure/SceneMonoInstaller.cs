using System;
using UnityEngine;
using Zenject;

public class SceneMonoInstaller : AbstractMonoInstaller
{
    [SerializeField] private GlobalSettings _globalSettings;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private PlayerBaseBehaviour _playerBase;
    [SerializeField] private LineRendererScript _lineRenderer;



    [SerializeField] private CanvasRefsContainer _canvasRefsContainerPrefab;
    private CanvasRefsContainer _canvasRefsContainer;

    public override void InstallBindings()
    {
        BindUnityComponentFromInstance(_audioSource);

        BindMonobehaviourService(_coroutineRunner);
        BindMonobehaviourService(_lineRenderer);
        BindMonobehaviourService(_playerBase);

        BindScriptableObject(_globalSettings.asteroidSettings);
        BindScriptableObject(_globalSettings.audioSetup);
        BindScriptableObject(_globalSettings.upgraderSettings);
        BindScriptableObject(_globalSettings.playerBehaviourSettings);
        BindScriptableObject(_globalSettings.missilefactorySettings);
        BindScriptableObject(_globalSettings.economicsManagerSettings);


        BindService<AudioService>();
        BindService<AsteroidFactory>();
        BindService<MissileFactory>();
        BindService<UpgradeService>();
        BindService<UIService>();
        BindService<GameStateMachine>();
        BindService<ParticleFactory>();
        BindService<EconomicService>();
        BindService<GameLoopService>();

        BindMonobehaviourServiceFromNew(_canvasRefsContainerPrefab);

        PostinitPhase();
    }

    private void PostinitPhase()
    {
        _lineRenderer.RedrawCircle(_globalSettings.upgraderSettings.initialRange);
        Container.QueueForInject(_playerBase);
    }
}
