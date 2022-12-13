using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; //player speed
    public Rigidbody2D rb; //rigidbody to have colisions
    public Vector2 moveInput; //bidirections of the player
    public float activeMoveSpeed; //state of player speed
    [Header ("State control")]
    public bool facingRight = true; // Is the player sprite facing to the right now?
    
    public Animator animator;
    public GameObject sprite;

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal"); //directions of the movement in X
        moveInput.y = Input.GetAxisRaw("Vertical"); //directions of the movement in Y

        moveInput.Normalize(); //it will make keep the same vector

        rb.velocity = moveInput * activeMoveSpeed; //velocity is the result of direction times current speed

        if(moveInput.x != 0 | moveInput.y != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        // Controls the sprite to always face the direction the player is moving
        Flip(sprite);
        
    }

    // Flips entire gameobject on X axis
    public void Flip(GameObject obj)
    {
        if ((!(moveInput.x < 0) || !facingRight) && (!(moveInput.x > 0) || facingRight)) return;
        facingRight = !facingRight;
        obj.transform.Rotate(new Vector3(0,180,0));
    }




}