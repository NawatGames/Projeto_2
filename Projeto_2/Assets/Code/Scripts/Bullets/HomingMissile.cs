using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float rotationSpeed = 200f;
    private Transform _player;
    private Rigidbody2D _rb;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        // Gets the player GameObject transform component
    }

    private void FixedUpdate()
    {
        var direction = (Vector2)_player.position - _rb.position;
        // the direction of the bullet to the player
        direction.Normalize();

        var right = transform.right;
        var rotateAmount = Vector3.Cross(direction, right).z;

        _rb.angularVelocity = -rotateAmount * rotationSpeed;
        _rb.velocity = right * bulletSpeed;
    }
}