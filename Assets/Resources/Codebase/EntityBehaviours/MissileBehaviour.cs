using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    private float speed;
    private int damage;
    private Transform _target;
    private Vector2 lookDirection;
    internal event Events.OnMissileDestroyed OnMissileDestroyed;
    private void Update()
    {
        if (_target != null)
            MoveObjectToTarget();
        else
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            SelfDestruct();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag is "Asteroid")
        {
            collision.gameObject.GetComponent<AsteroidBehaviour>().RecieveDamage(damage);
            OnMissileDestroyed?.Invoke(transform);
            Destroy(gameObject);
        }
    }

    private void MoveObjectToTarget() => 
        transform.Translate(speed * Time.deltaTime * (_target.position - transform.position).normalized, Space.World);

    public void Initialize(float _speed, int _damage)
    {
            speed = _speed;
            damage = _damage;                
    }
    public void SetTarget(Transform target)
    {
        _target = target;
        lookDirection = _target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
    }

    void SelfDestruct()
    {
        OnMissileDestroyed?.Invoke(transform);
        Destroy(gameObject);     
    }
}
