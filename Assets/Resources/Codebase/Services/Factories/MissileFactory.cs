using UnityEngine;

public class MissileFactory : AbstractMonoBehaviuorFactory<MissileBehaviour>
{
    private MissileBehaviour _missilePrefab;

    //product dependencies
    private AudioService _audioService;
    private ParticleFactory _particleFactory;
    private float _speed;
    private int damage;
 
    public MissileFactory(MissilefactorySettings missilefactorySettings, AudioService audioService, ParticleFactory particleFactory) : base()
    {
        _audioService = audioService;
        _particleFactory = particleFactory;
        _missilePrefab = missilefactorySettings.missilePrefab;
        _speed = missilefactorySettings.missileSpeed;
    }

    public void SetDamage(int _damage) 
        => damage = _damage;

    protected override MissileBehaviour ConstructNew()
    {
        MissileBehaviour missileBehaviour = GameObject.Instantiate(_missilePrefab, null);
        missileBehaviour.Initialize(_speed, damage, _audioService, _particleFactory);
        _audioService.PlayMissileLaunchSound();
        return missileBehaviour;
    }

    public override MissileBehaviour GetFromPool() 
        => _pool.Get();
}
