using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; //player speed
    public Rigidbody2D rb; //rigidbody to have colisions
    public Vector2 moveInput; //bidirections of the player
    public float activeMoveSpeed; //state of player speed

    public int direction;

    public int healthPlayer = 15;
    private int activeHealthPlayer;
    public HealthBar healthBar;
    
    public Shield shield;
    private Dash dash;
    
    [Header ("State control")]
    [SerializeField] public bool _facingLeft = false; // Is the player sprite facing to the right now?

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        activeHealthPlayer = healthPlayer;
        healthBar.SetHealthPlayer(healthPlayer);
        direction = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal"); //directions of the movement in X
        moveInput.y = Input.GetAxisRaw("Vertical"); //directions of the movement in Y

        moveInput.Normalize(); //it will make keep the same vector

        rb.velocity = moveInput * activeMoveSpeed; //velocity is the result of direction times current speed

        // Controls the sprite to always face the direction the player is moving
        if ((moveInput.x < 0) && (!_facingLeft))
        {
            Flip();
        }
        else if ((moveInput.x > 0) && (_facingLeft))
        {
            Flip();
        }

        if(direction == 0)
        {
            if(moveInput.x > 0)
            {
                direction = 1;
            }
            if(moveInput.x < 0)
            {
                direction = 2;
            }
            if(moveInput.y > 0)
            {
                direction = 3;
            }
            if(moveInput.y < 0)
            {
                direction = 4;
            }
        }

        
    }

    // Flips entire gameobject on X axis
    public void Flip()
    {
        _facingLeft = !_facingLeft;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent) && shield.shieldPlayer == true)
        {
            enemyComponent.TakeDamage(5);
        }
    }
}