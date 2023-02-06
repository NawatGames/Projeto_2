using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed; //dash speed of the player
    public float dashTime; //dashing time
    public float dashCooldown = 1f;  //time remaining to use it again
    private float _dashCounter; //state of time for dashing time
    private float _dashCoolCounter; //state of time for time remaining to use dash again
    [SerializeField] TrailRenderer tr; //trail render to design impulse
    public bool dashShieldPlayer;

    public PlayerMovement movement;
    public Shield shield;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        dashShieldPlayer = false;
        tr.emitting = false;
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) //if space is pressed...
        {//and the cooldown and dash time is equal to zero: player speed changes to dash, trail render is activated and the state
            // of time of dash start the countdown to reset
            if(_dashCoolCounter <= 0 && _dashCounter <= 0 && shield.isShielding == false)
            {
                if(movement.moveInput.y == 0f)
                {
                    movement.activeMoveSpeed = dashSpeed;
                    movement.rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation ;
                    dashShieldPlayer = true;
                    tr.emitting = true;
                    _dashCounter = dashTime;
                    animator.SetBool("Dashing", true);
                }

                if(movement.moveInput.x == 0f)
                {
                    movement.activeMoveSpeed = dashSpeed;
                    movement.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    dashShieldPlayer = true;
                    tr.emitting = true;
                    _dashCounter = dashTime;
                    animator.SetBool("Dashing", true);
                }
        
            }
        }

        if(_dashCounter > 0) //countdown of dashing time is reseting
        {
            _dashCounter -= Time.deltaTime;
 //but, the countdown is already reset(equal to zero): player speed changes to normal speed, trail render is deactivated and cooldown start its countdown
            if(_dashCounter <= 0)
            {
                movement.activeMoveSpeed = movement.moveSpeed;
                movement.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                dashShieldPlayer = false;
                tr.emitting = false;
                _dashCoolCounter = dashCooldown;
                animator.SetBool("Dashing", false);
                animator.SetBool("Dashing2", true);
            }
        }

        if(_dashCoolCounter > 0) //countdown of cooldown is reseting
        {
            _dashCoolCounter -= Time.deltaTime;
        }
    }
}
