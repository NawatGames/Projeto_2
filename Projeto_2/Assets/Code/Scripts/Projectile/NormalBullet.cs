using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class NormalBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifetime = 0.5f;

    private Vector3 _shootDir;

    private void Start()
    {
        _shootDir = Vector3.up.normalized; // Neutral shoot direction is upwards
    }

    private void OnEnable()
    {
        transform.rotation *= Quaternion.Euler(0, 0, -90);
        // Corrects the orientation of the bullet to X positive = 0 degrees
        // Now neutral shoot direction is right
    }

    private void Update()
    {
        bulletLifetime -= Time.deltaTime; // Controls the bullet lifeTime in seconds
        if (bulletLifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(_shootDir * Time.deltaTime * bulletSpeed, Space.Self);
        // Moves the bullet, based on the direction vector (relative to global axis) and speed in units by second
    }
}
