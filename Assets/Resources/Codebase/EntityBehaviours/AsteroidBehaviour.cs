using UnityEngine;
using Events;

public class AsteroidBehaviour : MonoBehaviour
{
    public float rotationSpeed;
    public int hitPonts;
    public int damage;
    public float speed;


    public event OnAsteroidDeath OnPaidDeath;
    public event OnAsteroidDeath OnUnpaidEvent;

    Transform playerBase;
    public Transform PlayerBase
    {
        private get => playerBase;
        set
        {
            if (playerBase == null)
                playerBase = value;
            else 
                Debug.LogWarning("Multiple attemts to set target position!");
        }
    }

    void Update()
    {
        MoveObject();
        RotateObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag is "Player")
        {
            collision.gameObject.GetComponent<PlayerBaseBehaviour>().RecieveDamage(damage);
            UnpaidDeath();
        }
    }
    public void RecieveDamage(int damage)
    {
        hitPonts -= damage;
        if (hitPonts <= 0) PaidDeath();
    }
    private void RotateObject() => 
        transform.eulerAngles += rotationSpeed * Time.deltaTime * Vector3.forward;


    private void MoveObject() => 
        transform.Translate(speed * Time.deltaTime * (playerBase.position - transform.position).normalized, Space.World);

    private void UnpaidDeath()
    {
        OnUnpaidEvent?.Invoke();
        Destroy(gameObject);
    }

    private void PaidDeath()
    {
        OnPaidDeath?.Invoke();
        Destroy(gameObject);
    }
}
