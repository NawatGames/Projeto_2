using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float _weaponPlayerDistance = 0.8f;
    [SerializeField] private float _swingSpeed = 360.0f;
    [SerializeField] public float _weaponDamage = 20.0f;

    private GameObject player;
    private float swingDuration;

    // Weapon swing animation
    public void SwingWeapon()
    {
        if (player.GetComponent<PlayerMovement>()._facingRight)
        {
            transform.RotateAround(player.transform.position, Vector3.forward, -_swingSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(player.transform.position, Vector3.forward, _swingSpeed * Time.deltaTime);
        }
    }

    // Setup
    public void Awake()
    {
        player = this.transform.parent.gameObject;
        this.transform.gameObject.SetActive(false);
    }

    // Checks if the animation has finished, then disables the weapon gameObject
    public void FixedUpdate()
    {
        if (swingDuration >= 0f)
        {
            swingDuration -= Time.deltaTime;
            SwingWeapon();
        }
        else
        {
            this.transform.gameObject.SetActive(false);
            player.GetComponent<PlayerMovement>()._attacking = false;
        }
    }

    // Setup
    public void OnEnable()
    {
        transform.position = player.transform.position + new Vector3(0f, _weaponPlayerDistance, 1);
        transform.rotation = Quaternion.identity;
        swingDuration = 1f/(_swingSpeed/180f);
    }
}
