using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttack;

    [SerializeField] public float attackCooldown;
    [SerializeField] public Transform attackPos;
    [SerializeField] public LayerMask whatIsEnemy;
    [SerializeField] public float attackRangeX;
    [SerializeField] public float attackRangeY;
    [SerializeField] public float damage;

    // Attacks whenever timeBetweenAttack (Cooldown of attack) goes lower than 0 and the E key is pressed,
    // calls the enemy TakeDamage() function, resets timeBetweenAttack, and reports wheter it's still attacking or not.
    public void Update()
    {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.GetComponent<PlayerMovement>()._attacking = true;
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<BossPart>().TakeDamage(damage);
                }
                timeBetweenAttack = attackCooldown;
            }
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
            this.GetComponent<PlayerMovement>()._attacking = false; // TODO: When animation is done, set this to false whenever animation is idle
        }
    }

    // Visualization of area of attack
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
