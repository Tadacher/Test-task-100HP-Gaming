using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    private float _speed;
    private int _damage;

    private ParticleFactory _particleFactory;
    private AudioService _audioService;
    private Transform _target;
    private Vector2 _lookDirection; //daffuck?

    public void Initialize(float speed, int damage, AudioService audioService, ParticleFactory particleFactory)
    {
        _speed = speed;
        _damage = damage;
        _particleFactory = particleFactory;
        _audioService = audioService;
    }
    public void SetTarget(Transform target)
    {
        _target = target;
        _lookDirection = _target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _lookDirection);
    }

    private void Update()
    {
        if (_target == null)
            SelfDestruct();

        MoveObjectToTarget();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag is not "Asteroid")
            return;

        collision.gameObject.GetComponent<AsteroidBehaviour>().RecieveDamage(_damage);
        DestroySelf();
    }

    private void MoveObjectToTarget() => 
        transform.Translate(_speed * Time.deltaTime * (_target.position - transform.position).normalized, Space.World);

    private void SelfDestruct() => 
        DestroySelf();

    private void DestroySelf()
    {
        _particleFactory.SpawnExplosion(transform);
        Destroy(gameObject);
    }
}
