using System;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool isShielding;
    public bool canShield = true;
    public float shieldConsumeRatio = 2.5f;
    public float penaltyRatio = 4;

    private Stamina _stamina;
    private PlayerMovement _playerMovement;
    public Animator animator;

    public void OnEnable()
    {
        isShielding = false;
        _stamina = GetComponent<Stamina>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.E) && _stamina.GetStamina() > 0 && canShield)
        {
            isShielding = true;
            _playerMovement.activeMoveSpeed = _playerMovement.moveSpeed / penaltyRatio;
            animator.SetBool("Shielding", true);
            _stamina.RemoveStamina(Time.deltaTime * shieldConsumeRatio);
        }
        else if (Input.GetKeyUp(KeyCode.E) && canShield)
        {
            isShielding = false;
            _playerMovement.activeMoveSpeed = _playerMovement.moveSpeed;
            animator.SetBool("Shielding", false);
        }
        else
        {
            _stamina.AddStamina(Time.deltaTime);
        }

        if (_stamina.GetStamina() == 0)
        {
            canShield = false;
            isShielding = false;
            animator.SetBool("Shielding", false);
            _playerMovement.activeMoveSpeed = _playerMovement.moveSpeed / penaltyRatio;
        }

        if (canShield == false && Math.Abs(_stamina.GetStamina() - _stamina.maxStamina) <= 0)
        {
            canShield = true;
            _playerMovement.activeMoveSpeed = _playerMovement.moveSpeed;
        }
    }
}