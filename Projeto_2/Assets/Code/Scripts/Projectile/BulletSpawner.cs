using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] private float fireRate;
    private float _delay;

    private void Start()
    {
        _delay = 1 / fireRate;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bullet, transform.position, bullet.transform.rotation);
        }
        
        _delay -= Time.deltaTime;
        if (_delay <= 0)
        {
            Instantiate(bullet, transform.position, bullet.transform.rotation);
            _delay = 1 / fireRate;
        }
    }
}
