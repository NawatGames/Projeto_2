using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour
{
    private float attackTime;
    public float attackCooldown;

    public Transform attackPosition;
    public LayerMask enemies;
    public float attackRange;
    public int damage;
    public Animator animator;

    public HealthBar healthBar;
    public int healthPlayer = 15;
    private int activeHealthPlayer;

    public Shield shield;

    void Start()
    {
        activeHealthPlayer = healthPlayer;
        healthBar.SetHealthPlayer(healthPlayer);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.C))
            {         
                if(attackTime <= 0)
                {
                    BasicAttack();
                    attackTime = attackCooldown;
                }
                
            }
        else
        {
            animator.SetBool("Attacking", false);
            attackTime -= Time.deltaTime;
        }
    }

    public void BasicAttack()
    {
        animator.SetBool("Attacking", true);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemies);
        foreach(Collider2D enemy in enemiesToDamage)
        {
            enemy.GetComponent<BossPart>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        activeHealthPlayer -= damage;
        healthBar.SetHealth(activeHealthPlayer);

        if(activeHealthPlayer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(((collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent)) && (shield.shieldPlayer == true)))
        {
            enemyComponent.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
