using System;
using UnityEngine;
using Zenject;

public class SceneMonoInstaller : MonoInstaller
{
    [SerializeField] GlobalSettings _globalSettings;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] CoroutineRunner _coroutineRunner;

    [SerializeField] private PlayerBaseBehaviour _playerBasePrefab;
    private PlayerBaseBehaviour _playerBase;

    [SerializeField] private CanvasRefsContainer _canvasRefsContainerPrefab;
    private CanvasRefsContainer _canvasRefsContainer;

    [SerializeField] private LineRendererScript _lineRendererPrefab;
    private LineRendererScript _lineRenderer;

    public override void InstallBindings()
    {
        BindUnityComponentFromInstance(_audioSource);
        BindMonobehaviourServiceFromInstance(_coroutineRunner);


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

        BindMonobehaviourService(ref _playerBase, _playerBasePrefab);
        BindMonobehaviourService(ref _canvasRefsContainer, _canvasRefsContainerPrefab);
        BindMonobehaviourService(ref _lineRenderer, _lineRendererPrefab);

        PostinitPhase();
    }

    private void BindMonobehaviourServiceFromInstance<TMonobehService>(TMonobehService instance) where TMonobehService : MonoBehaviour => 
        Container.Bind<TMonobehService>().FromInstance(instance).AsSingle();

    private void BindMonobehaviourService<TMonobehService>(ref TMonobehService field, TMonobehService prefab) where TMonobehService : MonoBehaviour
    {
        field = GameObject.Instantiate<TMonobehService>(prefab);
        Container.Bind<TMonobehService>().FromInstance(field).AsSingle();
    }
    private void BindUnityComponentFromInstance<TMonobehService>(TMonobehService instance) where TMonobehService : Component => 
        Container.Bind<TMonobehService>().FromInstance(instance).AsSingle();

    private void BindScriptableObject<TScriptableObject>(TScriptableObject scriptableObject) where TScriptableObject : ScriptableObject => 
        Container.Bind<TScriptableObject>().FromInstance(scriptableObject).AsSingle();

    private void BindService<TService>() => Container.Bind<TService>().AsSingle().NonLazy();

    private void PostinitPhase()
    {
        _lineRenderer.RangeLevelChangedHandlers.Invoke(_globalSettings.upgraderSettings.initialRange);
        Container.QueueForInject(_playerBase);
    }
}
