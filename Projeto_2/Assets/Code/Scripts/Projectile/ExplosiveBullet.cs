using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ExplosiveBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifetime = 0.5f;
    
    [SerializeField] private GameObject fragmentObject;
    [SerializeField] private int numberOfFragments;

    private Vector3 _shootDir;
    
    // public UnityAction OnDestroy; 

    private void OnEnable()
    {
        var player = GameObject.FindGameObjectWithTag("Player"); // Finds the player gameObject in the scene
        
        var bulletPosition = transform.position;
        var playerPosition = player.transform.position;
        _shootDir = (playerPosition - bulletPosition).normalized; // Defines the direction of the bullet to the player

        var angle = Mathf.Atan2(_shootDir.y, _shootDir.x) * Mathf.Rad2Deg; // Angle in degrees to rotate gameObject
        transform.rotation *= Quaternion.Euler(0, 0, angle-90);
    }

    private void Update()
    {
        bulletLifetime -= Time.deltaTime; // Controls the bullet lifeTime in seconds
        if (bulletLifetime <= 0)
        {
            Destroy(this.gameObject);
            Explode();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(_shootDir * Time.deltaTime * bulletSpeed, Space.World);
        // Moves the bullet, based on the direction vector (relative to global axis) and speed in units by second
    }

    private void Explode()
    {
        for (var i = 0; i < numberOfFragments; i++) // Repeats (the number of fragments) times
        {
            Instantiate(fragmentObject, transform.position, Quaternion.Euler(0, 0, 360 / numberOfFragments * i));
            // Creates a fragment on the same position as the gameObject and rotated by a fraction of 360 degrees
        }
    }
}
