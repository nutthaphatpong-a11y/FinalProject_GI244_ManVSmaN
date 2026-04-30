using UnityEngine;
using System.Collections;

public class Guardian : MonoBehaviour
{
    [Header("Basic")]
    public float hp = 5f;
    public int cost = 50;

    [Header("Attack")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float damage = 1f;
    public float fireRate = 1f;
    public float attackRange = 10f;

    [Header("Size")]
    public int sizeX = 1;
    public int sizeZ = 1;

    [Header("Detection")]
    public LayerMask enemyLayer;

    [Header("Animation")]
    public Animator anim;
    public string weaponTypeParameter = "WeaponType_int";

    // internal
    private float shootTimer;
    private Tile myTile;

    private float baseDamage;
    private float baseFireRate;
    private float baseRange;

    private Renderer[] renderers;



    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);

        if (anim != null)
            anim.SetInteger(weaponTypeParameter, 1);

        // เก็บค่า base
        baseDamage = damage;
        baseFireRate = fireRate;
        baseRange = attackRange;

        // เก็บ renderer ทั้งหมด (รองรับหลาย mesh)
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= fireRate && HasEnemyInLane())
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.SetDamage(damage);
        }
    }

    bool HasEnemyInLane()
    {
        Ray ray = new Ray(firePoint.position, firePoint.right);

        Debug.DrawRay(firePoint.position, firePoint.right * attackRange, Color.red);

        return Physics.Raycast(ray, attackRange, enemyLayer);
    }


    public void TakeDamage(float dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ClearTiles();
        Destroy(gameObject);
    }

    public void RemoveSelf()
    {
        ClearTiles();
        Destroy(gameObject);
    }

    // เคลียร์ช่อง
    void ClearTiles()
    {
        if (myTile == null) return;

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                Tile t = GridManager.instance.GetTile(myTile.x + x, myTile.z + z);

                if (t != null)
                    t.isOccupied = false;
            }
        }
    }

    public void SetTile(Tile tile)
    {
        myTile = tile;
    }

    // ⚡ POWER UP
    public void ApplyPowerUp(PowerUpType type, float value, float duration)
    {
        StartCoroutine(BuffRoutine(type, value, duration));
    }

    IEnumerator BuffRoutine(PowerUpType type, float value, float duration)
    {
        
        ApplyBuff(type, value);

        
        SetColor(GetColor(type));

        yield return new WaitForSeconds(duration);

        
        ResetStats();
        SetColor(Color.white);
    }

    void ApplyBuff(PowerUpType type, float value)
    {
        switch (type)
        {
            case PowerUpType.Damage:
                damage += value;
                break;

            case PowerUpType.FireRate:
                fireRate -= value;
                break;

            case PowerUpType.Range:
                attackRange += value;
                break;
        }
    }

    void ResetStats()
    {
        damage = baseDamage;
        fireRate = baseFireRate;
        attackRange = baseRange;
    }


    Color GetColor(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.Damage: return Color.red;
            case PowerUpType.FireRate: return Color.yellow;
            case PowerUpType.Range: return Color.green;
        }

        return Color.white;
    }

    void SetColor(Color color)
    {
        foreach (Renderer r in renderers)
        {
            r.material.color = color;
        }
    }
}