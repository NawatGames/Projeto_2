using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour
{
    private float attackTime;
    public float attackCooldown;

    public Vector3 attackOffset;
    public LayerMask enemies;
    public float attackRange;
    public int damage;
    public Animator animator;

    public HealthBar healthBar;
    public int healthPlayer = 15;
    private int _activeHealthPlayer;

    public Shield shield;
    public PlayerMovement playerMovement;

    private Vector3 attackPosition;

    void Start()
    {
        _activeHealthPlayer = healthPlayer;
        healthBar.SetHealthPlayer(healthPlayer);
        playerMovement = GetComponent<PlayerMovement>();
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.facingRight)
        {
            attackPosition = transform.position + attackOffset;
        }
        else
        {
            attackPosition = transform.position - attackOffset;
        }
        // attackPosition = transform.position + (attackOffset * playerMovement.moveInput.x);
        
        if(attackTime <= 0)
        { 
            if(Input.GetKey(KeyCode.C))
            {
                animator.SetBool("Attacking", true);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<BossPart>().TakeDamage(damage);
                }
            }
            else
            {
                animator.SetBool("Attacking", false);
            }
            attackTime = attackCooldown;
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        _activeHealthPlayer -= damage;
        healthBar.SetHealth(_activeHealthPlayer);

        if(_activeHealthPlayer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(((collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent)) && (shield.shieldPlayer == true)))
        {
            enemyComponent.TakeDamage(3);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition, attackRange);
    }
}
