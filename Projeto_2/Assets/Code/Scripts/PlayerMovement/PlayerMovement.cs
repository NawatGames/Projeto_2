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

    public float dashTime = .5f; //dashing time
    public float dashCooldown = 1f;  //time remaining to use it again

    private float dashCounter; //state of time for dashing time
    private float dashCoolCounter; //state of time for time remaining to use dash again

    [SerializeField] TrailRenderer tr; //trail render to design impulse

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal"); //directions of the movement in X
        moveInput.y = Input.GetAxisRaw("Vertical"); //directions of the movement in Y

        moveInput.Normalize(); //it will make keep the same vector

        rb.velocity = moveInput * activeMoveSpeed; //velocity is the result of direction times current speed

        if(Input.GetKeyDown(KeyCode.Space)) //if space is pressed...
        {//and the cooldown and dash time is equal to zero: player speed changes to dash, trail render is activated and the state
        // of time of dash start the countdown to reset
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                tr.emitting = true;
                dashCounter = dashTime;
            }
        }

        if(dashCounter > 0) //countdown of dashing time is reseting
        {
            dashCounter -= Time.deltaTime;
 //but, the countdown is already reset(equal to zero): player speed changes to normal speed, trail render is deactivated and cooldown start its countdown
            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                tr.emitting = false;
                dashCoolCounter = dashCooldown;
            }
        }

        if(dashCoolCounter > 0) //countdown of cooldown is reseting
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }
}