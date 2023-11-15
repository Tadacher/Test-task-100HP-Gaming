using UnityEngine;
using Zenject;

public class AbstractMonoInstaller : MonoInstaller
{

    protected ConcreteIdArgConditionCopyNonLazyBinder BindMonobehaviourService<TMonobehService>(TMonobehService instance) where TMonobehService : MonoBehaviour =>
        Container.Bind<TMonobehService>().FromInstance(instance).AsSingle();

    protected void BindMonobehaviourServiceFromNew<TMonobehService>(TMonobehService prefab) where TMonobehService : MonoBehaviour
    {
        TMonobehService service = GameObject.Instantiate(prefab);
        Container.Bind<TMonobehService>().FromInstance(service).AsSingle();
    }

    protected void BindScriptableObject<TScriptableObject>(TScriptableObject scriptableObject) where TScriptableObject : ScriptableObject
        => Container.Bind<TScriptableObject>().FromInstance(scriptableObject).AsSingle();

    protected void BindService<TService>() 
        => Container.Bind<TService>().AsSingle().NonLazy();
    protected void BindUnityComponentFromInstance<TMonobehService>(TMonobehService instance) where TMonobehService : Component 
        => Container.Bind<TMonobehService>().FromInstance(instance).AsSingle();

}