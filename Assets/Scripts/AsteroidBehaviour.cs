using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class AsteroidBehaviour : MonoBehaviour
{
    internal float rotationSpeed;
    internal int hitPonts;
    internal int damage;
    internal float speed;

    
    internal event AsteroidDeath OnPaidDeath;
    internal event AsteroidDeath OnUnpaidEvent;

    Transform playerBase;
    public Transform PlayerBase
    {
        private get 
        {
            return playerBase;
        }
        set
        {
            if (playerBase == null) playerBase = value;
            else Debug.LogWarning("Multiple attemts to set target position!");
        }
    }

    void Update()
    {
        transform.Translate((playerBase.position - transform.position).normalized * speed * Time.deltaTime, Space.World);
        transform.eulerAngles += Vector3.forward * rotationSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag is "Player")
        {
            collision.gameObject.GetComponent<PlayerBaseBehaviour>().RecieveDamage(damage);
            UnpaidDeath();
        }
    }

    void UnpaidDeath()
    {
        OnUnpaidEvent?.Invoke();
        Destroy(gameObject);
    }
    void PaidDeath()
    {
        OnPaidDeath?.Invoke();
        Destroy(gameObject);
    }
    internal void RecieveDamage(int damage)
    {
        hitPonts -= damage;
        if (hitPonts <= 0) PaidDeath();
    }
}
