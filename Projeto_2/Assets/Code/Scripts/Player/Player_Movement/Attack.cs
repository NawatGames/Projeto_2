using UnityEngine;

public class Attack : MonoBehaviour
{
    private float _attackTime;
    public Vector3 attackOffset;
    public float attackRange;
    public float attackCooldown;
    public int damage;
    public LayerMask enemies;
    public Animator animator;
    
    private Health _health;
    private Shield _shield;
    private PlayerMovement _playerMovement;
    private Vector3 _attackPosition;

    private void OnEnable()
    {
        _health = GetComponent<Health>();
        _shield = GetComponent<Shield>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_playerMovement.facingRight)
        {
            _attackPosition = transform.position + attackOffset;
        }
        else
        {
            _attackPosition = transform.position - attackOffset;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {         
            if (_attackTime <= 0)
            {
                    BasicAttack(); 
                    _attackTime = attackCooldown;
            }
        }
        else
        {
            animator.SetBool("Attacking", false);
            _attackTime -= Time.deltaTime;
        }
    }

    private void BasicAttack()
    {
        animator.SetBool("Attacking", true);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, attackRange, enemies);
        foreach(Collider2D enemy in enemiesToDamage)
        {
            enemy.GetComponent<BossPart>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int value)
    {
        _health.RemoveHealth(value);

        if(_health.GetHealth() <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(((collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent)) && _shield.isShielding))
        {
            enemyComponent.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition, attackRange);
    }
}
