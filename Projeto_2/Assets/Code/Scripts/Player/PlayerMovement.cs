using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("State control")]
    public float moveSpeed; //player speed
    public float activeMoveSpeed; //state of player speed
    public Vector2 moveInput; //bidirections of the player
    public bool facingRight = true; // Is the player sprite facing to the right now?
    
    [Header ("Component References")]
    public Animator animator;
    public GameObject sprite;
    public ParticleSystem dust;
    public Rigidbody2D rb; //rigidbody to have colisions

    private void OnEnable()
    {
        activeMoveSpeed = moveSpeed;
        // dust.Stop();
    }

    private void Update()
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
    private void Flip(GameObject obj)
    {
        if ((!(moveInput.x < 0) || !facingRight) && (!(moveInput.x > 0) || facingRight)) return;
        facingRight = !facingRight;
        obj.transform.Rotate(new Vector3(0,180,0));
    }
}