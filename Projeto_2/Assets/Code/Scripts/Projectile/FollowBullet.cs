using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowBullet : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    public GameObject player;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifetime = 0.5f;
    private Vector3 _bulletPosition;
    private Vector3 _playerPosition;
    private float _sin;
    private float _cos;
    public float angle;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        
        _bulletPosition = GetComponent<Transform>().position;
        _playerPosition = player.transform.position;
        
        Trigonometrics(_playerPosition, _bulletPosition);
    }

    private void Trigonometrics(Vector3 target, Vector3 bullet)
    {
        var catOp = target.y - bullet.y;
        var catAd = target.x - bullet.x;

        var hip = math.sqrt(math.pow((catOp), 2) + math.pow((catAd), 2));
        
        _sin = catOp / hip;
        _cos = catAd / hip;

        if (_sin < 0)
        {
            angle = -math.acos(catAd / hip);
        }
        else angle = math.acos(catAd / hip);
    }

    private void Update()
    {
        // _bulletPosition = GetComponent<Transform>().position;
        // _playerPosition = player.transform.position;
        //
        // Trigonometrics(_playerPosition, _bulletPosition);

        _transform.rotation = Quaternion.Euler(0, 0, (angle * 180 / math.PI)-90);

        bulletLifetime -= Time.deltaTime;
        if (bulletLifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.position += new Vector2(bulletSpeed*_cos, bulletSpeed*_sin) * Time.deltaTime;
    }
}
