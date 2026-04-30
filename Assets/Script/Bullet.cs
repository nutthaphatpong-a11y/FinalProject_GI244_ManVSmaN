using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private float damage;

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        CheckOutOfBounds();
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponentInParent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void CheckOutOfBounds()
    {
        if (transform.position.x > 13f)
        {
            Destroy(gameObject);
        }
    }
}