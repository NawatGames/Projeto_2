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
    [SerializeField] private GameObject shieldImage;
    public Stamina stamina;
    public PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        shieldPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(shieldCoolCounter <= 0 && shieldCounter <= 0)
            {
                shieldPlayer = true;
                movement.activeMoveSpeed = movement.moveSpeed / 4;
                shieldImage.SetActive(true);
                shieldCounter = shieldTime;
                stamina.SetStaminaPlayer(shieldTime);
            }
        }

        if(shieldCounter > 0)
        {
            shieldCounter -= Time.deltaTime;
            stamina.SetStamina(shieldCounter);

            if(shieldCounter <= 0)
            {
                shieldPlayer = false;
                shieldImage.SetActive(false);
                movement.activeMoveSpeed = movement.moveSpeed;
                shieldCoolCounter = shieldCooldown;
                stamina.SetStaminaPlayer(shieldCooldown);
            }
        }

        if(shieldCoolCounter > 0)
        {
            shieldCoolCounter -= Time.deltaTime;
            stamina.SetStamina(shieldCoolCounter);
        }
    }
}
