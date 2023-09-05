using Events;
using UnityEngine;

public class ParticleFactory : AbstractMonoBehaviuorFactory<ParticleBehaviour>
{
    public OnMissileDestroyed _onMissileDestroyedGfxHandlers;
    private ParticleBehaviour _explosionPrefab;

    public ParticleFactory() : base()
    {
        _explosionPrefab = Resources.Load<ParticleBehaviour>("Prefabs/Gfx/ExplosionContainer");
        _onMissileDestroyedGfxHandlers += SpawnExplosion;
    }

    void SpawnExplosion(Transform target) => 
        GameObject.Instantiate(_explosionPrefab, target.position, target.rotation);

    public override ParticleBehaviour GetFromPool() => 
        _pool.Get();

    protected override ParticleBehaviour ConstructNew() => 
        GameObject.Instantiate(_explosionPrefab, Vector3.zero, Quaternion.identity);
}
