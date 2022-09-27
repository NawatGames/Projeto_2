using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifetime = 0.5f;

    private Vector3 _shootDir;

    private void Start()
    {
        _shootDir = Vector3.right.normalized; // Neutral shoot direction is right
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
        transform.Translate(_shootDir * (Time.deltaTime * bulletSpeed), Space.Self);
        // Moves the bullet, based on the direction vector (relative to itself) and speed in units by second
    }
}
