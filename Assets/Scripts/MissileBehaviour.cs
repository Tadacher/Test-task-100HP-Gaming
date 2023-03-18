using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    float speed;
    int damage;
    bool selfDestruct;
    Transform target;
    Vector2 lookDirection;
    internal event Events.MissileDestroyed OnMissileDestroyed;
    private void Update()
    {
        if (target != null) transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime, Space.World);
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
    internal void Initialize(float _speed, int _damage, Transform _target)
    {
            speed = _speed;
            damage = _damage;
            target = _target;
            lookDirection = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
    }
    void SelfDestruct()
    {
        OnMissileDestroyed?.Invoke(transform);
        Destroy(gameObject);     
    }
}
