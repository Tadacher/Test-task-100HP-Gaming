using UnityEngine;

public class ParticleFactory : AbstractMonoBehaviuorFactory<ParticleBehaviour>
{
    private ParticleBehaviour _explosionPrefab;

    public ParticleFactory() : base()
    {
        _explosionPrefab = Resources.Load<ParticleBehaviour>("Prefabs/Gfx/ExplosionContainer");
    }

    public void SpawnExplosion(Transform target) => 
        GameObject.Instantiate(_explosionPrefab, target.position, target.rotation);

    public override ParticleBehaviour GetFromPool() => 
        _pool.Get();

    protected override ParticleBehaviour ConstructNew() => 
        GameObject.Instantiate(_explosionPrefab, Vector3.zero, Quaternion.identity);
}
