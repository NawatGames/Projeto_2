using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerMovement player;
    public Shield shield;
    public Dash dash;
    public int healthEnemy = 15;
    private int activeHealthEnemy;
    public HealthBar healthBar;
    public Attack attack;


    void Start()
    {
        activeHealthEnemy = healthEnemy;
        healthBar.SetHealthPlayer(healthEnemy);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Attack>(out Attack enemyComponent))
        {
            if(!shield.shieldPlayer && !dash.dashShieldPlayer)
            {
                enemyComponent.TakeDamage(3);
            }
        }
    }

        public void TakeDamage(int damage)
    {
        activeHealthEnemy -= damage;
        healthBar.SetHealth(activeHealthEnemy);

        if(activeHealthEnemy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
