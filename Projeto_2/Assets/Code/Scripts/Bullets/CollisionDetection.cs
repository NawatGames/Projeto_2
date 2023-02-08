using System;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public float damage;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.gameObject;

        if (!player.CompareTag("Player")) return;
        if (!player.GetComponent<Shield>().isShielding)
        {
            player.GetComponent<Health>().RemoveHealth(damage);
            Debug.Log(damage+" DAMAGE!");
            Destroy(gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Debug.Log("PROJECTILE DESTROYED");
            Destroy(gameObject);
        }
    }
}