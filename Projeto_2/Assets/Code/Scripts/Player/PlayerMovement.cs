using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; //player speed
    public Rigidbody2D rb; //rigidbody to have colisions
    private Vector2 moveInput; //bidirections of the player

    private float activeMoveSpeed; //state of player speed
    public float dashSpeed; //dash speed of the player

    private int direction;

    public float dashTime; //dashing time
    public float dashCooldown = 1f;  //time remaining to use it again
    private float dashCounter; //state of time for dashing time
    private float dashCoolCounter; //state of time for time remaining to use dash again
    [SerializeField] private GameObject dashShieldImage;
    [SerializeField] TrailRenderer tr; //trail render to design impulse
    public bool dashShieldPlayer;

    public int healthPlayer = 15;
    private int activeHealthPlayer;
    public HealthBar healthBar;

    public bool shieldPlayer;
    public float shieldTime = 5f;
    public float shieldCooldown = 2.5f;
    private float shieldCounter;
    private float shieldCoolCounter;
    [SerializeField] private GameObject shieldImage;
    
    [Header ("State control")]
    [SerializeField] public bool _facingRight = false; // Is the player sprite facing to the right now?
    [SerializeField] public bool _attacking = false; // Is the plpayer attacking now?
    
    private GameObject _playerWeapon;

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        activeHealthPlayer = healthPlayer;
        healthBar.SetHealthPlayer(healthPlayer);
        shieldPlayer = false;
        dashShieldPlayer = false;
        tr.emitting = false;
        _playerWeapon = transform.Find("Weapon").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal"); //directions of the movement in X
        moveInput.y = Input.GetAxisRaw("Vertical"); //directions of the movement in Y

        moveInput.Normalize(); //it will make keep the same vector

        rb.velocity = moveInput * activeMoveSpeed; //velocity is the result of direction times current speed

        // Controls the sprite to always face the direction the player is moving
        if ((moveInput.x < 0) && (!_facingRight) && (!_attacking))
        {
            Flip();
        }
        else if ((moveInput.x > 0) && (_facingRight) && (!_attacking))
        {
            Flip();
        }

        // Attacks on key input
        if (Input.GetKeyDown(KeyCode.F))
        {
            _attacking = true;
            _playerWeapon.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Space)) //if space is pressed...
        {//and the cooldown and dash time is equal to zero: player speed changes to dash, trail render is activated and the state
        // of time of dash start the countdown to reset
            if(dashCoolCounter <= 0 && dashCounter <= 0 && shieldPlayer == false)
            {
                if(moveInput.y == 0f)
                {
                    activeMoveSpeed = dashSpeed;
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                    dashShieldImage.SetActive(true);
                    dashShieldPlayer = true;
                    tr.emitting = true;
                    dashCounter = dashTime;   
                }

                if(moveInput.x == 0f)
                {
                    activeMoveSpeed = dashSpeed;
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                    dashShieldImage.SetActive(true);
                    dashShieldPlayer = true;
                    tr.emitting = true;
                    dashCounter = dashTime;   
                }
        
            }
        }

        if(dashCounter > 0) //countdown of dashing time is reseting
        {
            dashCounter -= Time.deltaTime;
 //but, the countdown is already reset(equal to zero): player speed changes to normal speed, trail render is deactivated and cooldown start its countdown
            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                rb.constraints = RigidbodyConstraints2D.None;
                dashShieldImage.SetActive(false);
                dashShieldPlayer = false;
                tr.emitting = false;
                dashCoolCounter = dashCooldown;
            }
        }

        if(dashCoolCounter > 0) //countdown of cooldown is reseting
        {
            dashCoolCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(shieldCoolCounter <= 0 && shieldCounter <= 0)
            {
                shieldPlayer = true;
                activeMoveSpeed = moveSpeed / 4;
                shieldImage.SetActive(true);
                shieldCounter = shieldTime;
            }
        }

        if(shieldCounter > 0)
        {
            shieldCounter -= Time.deltaTime;

            if(shieldCounter <= 0)
            {
                shieldPlayer = false;
                shieldImage.SetActive(false);
                activeMoveSpeed = moveSpeed;
                shieldCoolCounter = shieldCooldown;
            }
        }

        if(shieldCoolCounter > 0)
        {
            shieldCoolCounter -= Time.deltaTime;
        }
    }

    // Flips entire gameobject on X axis
    public void Flip()
    {
        _facingRight = !_facingRight;

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
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent) && shieldPlayer == true)
        {
            enemyComponent.TakeDamage(5);
        }
    }
}