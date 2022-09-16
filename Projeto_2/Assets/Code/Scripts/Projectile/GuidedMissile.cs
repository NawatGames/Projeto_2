using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GuidedMissile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifetime = 0.5f;
    
    private Vector3 _shootDir;
    private GameObject _player;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); // Finds the player gameObject in the scene
    }

    private void Update()
    {
        var bulletPosition = transform.position;
        var playerPosition = _player.transform.position;
        _shootDir = (playerPosition - bulletPosition).normalized; // Defines the direction of the bullet to the player

        var angle = Mathf.Atan2(_shootDir.y, _shootDir.x) * Mathf.Rad2Deg; // Angle in degrees to rotate gameObject
        transform.rotation = Quaternion.Euler(0, 0, angle-90);
        
        bulletLifetime -= Time.deltaTime; // Controls the bullet lifeTime in seconds
        if (bulletLifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(_shootDir * Time.deltaTime * bulletSpeed, Space.World);
        // Moves the bullet, based on the direction vector (relative to global axis) and speed in units by second
    }
}
