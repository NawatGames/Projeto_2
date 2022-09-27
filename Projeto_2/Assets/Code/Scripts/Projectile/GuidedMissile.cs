using UnityEngine;

public class GuidedMissile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private GameObject _player;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); // Finds the player gameObject in the scene
    }

    private void Update()
    {
        var bulletPosition = transform.position;
        var playerPosition = _player.transform.position;
        var shootDir = playerPosition - bulletPosition;

        var angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg; // Angle in degrees to rotate gameObject
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right.normalized * (Time.deltaTime * bulletSpeed), Space.Self);
        // Moves the bullet, based on the direction vector (relative to itself) and speed in units by second
    }
}