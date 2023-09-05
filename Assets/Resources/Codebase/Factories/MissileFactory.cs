using UnityEngine;
using Events;

public class MissileFactory : AbstractMonoBehaviuorFactory<MissileBehaviour>
{
    private event OnMissileLaunched _onMissileLaunched;
    public MissileBehaviour LastGetted { get; private set; }

    public OnMissileDestroyed _onMissileDestroyedHandlers;

    private GameObject _missilePrefab;
    private float _speed;
    private int damage;
    private MissileBehaviour missileBehaviour;


    public MissileFactory(MissilefactorySettings missilefactorySettings, AudioService audioService, ParticleFactory particleFactory) : base()
    {
        _missilePrefab = missilefactorySettings.missilePrefab;
        _speed = missilefactorySettings.missileSpeed;
        _onMissileLaunched += audioService.MissileLaunchedSoundHandlers;
        _onMissileDestroyedHandlers += particleFactory._onMissileDestroyedGfxHandlers;
    }

    internal void SetDamage(int _damage) => damage = _damage;

    protected override MissileBehaviour ConstructNew()
    {
        missileBehaviour = GameObject.Instantiate(_missilePrefab, null).GetComponent<MissileBehaviour>();
        missileBehaviour.OnMissileDestroyed += _onMissileDestroyedHandlers;
        _onMissileLaunched?.Invoke();
        return missileBehaviour;
    }

    public override MissileBehaviour GetFromPool() => _pool.Get();
}
