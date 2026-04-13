using UnityEngine;

public class Guardian : MonoBehaviour
{
    public float hp = 5f;
    public int cost = 50;
    [Header("Attack")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float timer;

    public float attackRange = 10f;
    public LayerMask enemyLayer;
    public string weaponTypeParameter = "WeaponType_int";
    private Tile myTile;
    public Animator anim;

    public void Start()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
        anim.SetInteger(weaponTypeParameter, 1);
    }
    void Update()
    {
        timer += Time.deltaTime;

        // 🔥 ยิงเฉพาะเมื่อมีศัตรูในเลน
        if (timer >= fireRate && HasEnemyInLane())
        {
            Shoot();
            timer = 0f;
        }
    }

    bool HasEnemyInLane()
    {
        Debug.DrawRay(firePoint.position, firePoint.right * attackRange, Color.red);
        Ray ray = new Ray(firePoint.position, firePoint.right);

        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, enemyLayer))
        {
            return true; // มีศัตรูข้างหน้า
        }

        return false;
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void SetTile(Tile tile)
    {
        myTile = tile;
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
        if (myTile != null)
        {
            myTile.isOccupied = false;
        }

        Destroy(gameObject);
    }
}