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
    
    private Shield _shield;
    private PlayerMovement _playerMovement;
    private Vector3 _attackPosition;
    
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _shield = GetComponent<Shield>();
        _playerMovement = GetComponent<PlayerMovement>();
        _audioSource = GetComponent<AudioSource>();
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

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!(_attackTime <= 0) || _shield.isShielding) return;
            BasicAttack(); 
            _attackTime = attackCooldown;
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
        var partsToDamage = Physics2D.OverlapCircleAll(_attackPosition, attackRange, enemies);
        foreach(var part in partsToDamage)
        {
            part.GetComponent<Health>().RemoveHealth(damage);
        }
        _audioSource.Play();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition, attackRange);
    }
}
