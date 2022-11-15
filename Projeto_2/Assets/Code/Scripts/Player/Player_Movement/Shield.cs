using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool shieldPlayer;
    public float shieldTime = 5f;
    public float shieldCooldown = 2.5f;
    private float shieldCounter;
    private float shieldCoolCounter;

    public Stamina stamina;
    public PlayerMovement movement;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        shieldPlayer = false;
        stamina.canvasStamina.SetActive(false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            if(shieldCoolCounter <= 0 && shieldCounter <= 0)
            {
                shieldPlayer = true;
                stamina.canvasStamina.SetActive(true);
                movement.activeMoveSpeed = movement.moveSpeed / 4;
                shieldCounter = shieldTime;
                stamina.SetStaminaPlayer(shieldTime);
                animator.SetBool("Shielding", true);
            }
        }

        if(shieldCounter > 0)
        {
            shieldCounter -= Time.deltaTime;
            stamina.SetStamina(shieldCounter);

            if(shieldCounter <= 0)
            {
                shieldPlayer = false;
                movement.activeMoveSpeed = movement.moveSpeed;
                shieldCoolCounter = shieldCooldown;
                stamina.SetStaminaPlayer(shieldCooldown);
                animator.SetBool("Shielding", false);
            }
        }

        if(shieldCoolCounter > 0)
        {
            shieldCoolCounter -= Time.deltaTime;
            stamina.SetStamina(shieldCoolCounter);
        }
    }
}
