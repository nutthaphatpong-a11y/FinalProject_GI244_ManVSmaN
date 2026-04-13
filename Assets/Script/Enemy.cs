using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 1f;
    public float maxHP = 5f;
    public int Dropmoney = 20;
    public float currentHP;

    [Header("Attack")]
    public float attackDamage = 1f;
    public float attackRate = 1f;
    private float attackTimer;

    private bool isAttacking = false;
    public Animator animator;
    private Guardian target;

    void Start()
    {
        currentHP = maxHP;
        transform.rotation = Quaternion.Euler(0, -90, 0);
        if (animator != null)
        {
            animator.SetFloat("Speed_f", 1f);
        }
    }

    void Update()
    {
        if (isAttacking)
        {
            
            if (target == null)
            {
                isAttacking = false;
            }
        }

        if (!isAttacking)
        {
            Move();
        }
        else
        {
            Attack();
        }
    }

    void Move()
    {
        Vector3 direction = Vector3.left;

        
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackRate)
        {
            attackTimer = 0f;

            if (target != null)
            {
                target.TakeDamage(attackDamage);
            }
        }
    }

    
    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            animator.SetBool("Death_b", true);
            StartCoroutine(Die());
        }
    }


    IEnumerator Die()
    {
        animator.SetBool("Death_b", true);
        GameManager.instance.AddMoney(Dropmoney);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    //void Die()
    //{
    //    Destroy(gameObject);

    //}

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guardian"))
        {
            isAttacking = true;
            target = other.GetComponent<Guardian>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Guardian"))
        {
            isAttacking = false;
            target = null;
        }
    }
}