using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] private GameObject player;
    [SerializeField] private float fireRate;
    private float _delay;

    private void Start()
    {
        _delay = 1 / fireRate;
        SpawnBullet(bullet);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     Instantiate(bullet);
        // }
        
        _delay -= Time.deltaTime;
        if (_delay <= 0)
        {
            SpawnBullet(bullet);
            _delay = 1 / fireRate;
        }
    }

    private void SpawnBullet(GameObject obj)
    {
        if (obj.GetComponent<FollowBullet>() != null)
        {
            obj.GetComponent<FollowBullet>().player = player;
        }

        Instantiate(obj);
    }
}
