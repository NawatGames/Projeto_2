using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifetime = 0.5f;
    
    private Rigidbody2D _rb;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bulletLifetime -= Time.deltaTime; // Controls the bullet lifeTime in seconds
        if (bulletLifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.right * bulletSpeed;
    }
}