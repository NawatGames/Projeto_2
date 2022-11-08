using System;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public float damage;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.gameObject;
    
        if  (player.CompareTag("Player"))
        { 
            Debug.Log(damage+" damage!");
            Destroy(gameObject);
        }
    }
}
