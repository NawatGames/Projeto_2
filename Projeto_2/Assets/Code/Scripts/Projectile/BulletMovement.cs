using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class BulletMovement : MonoBehaviour
{
    // private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float bulletSpeed = 1f;
    [SerializeField] private float randomAngleDegrees;
    private float _randomAngleRadians;
    [SerializeField] private float bulletLifetime = 5f;

    private void Start()
    {
        // _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        randomAngleDegrees = Random.Range(0, 360);
        _randomAngleRadians = randomAngleDegrees * math.PI /180;
        transform.Rotate(0, 0, randomAngleDegrees-90, Space.Self);
    }

    private void Update()
    {
        bulletLifetime -= Time.deltaTime;
    
        if (bulletLifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.position += new Vector2(bulletSpeed*math.cos(_randomAngleRadians), bulletSpeed*math.sin(_randomAngleRadians)) * Time.deltaTime;
    }
}
