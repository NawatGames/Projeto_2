using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    public float damage;
    public float delay = 1f;

    private float _t;

    public void Start()
    {
        _t = 0;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        var player = col.gameObject;

        _t -= Time.deltaTime;
        
        if (player.CompareTag("Player") && _t <= 0)
        { 
            Debug.Log(damage+" damage!");
            _t = delay;
        }
    }
}
