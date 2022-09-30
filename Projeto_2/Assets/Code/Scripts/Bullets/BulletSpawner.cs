using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] private float fireRate;
    
    private float _delay;
    private readonly List<Transform> _spawnLocations = new List<Transform>();
    private Transform _player;
    private Quaternion _toRotation;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        // Gets the player Transform component
        
        _delay = 1 / fireRate; // Converts fire rate to bullets per second

        foreach (Transform tr in transform)
        {
            _spawnLocations.Add(tr);
        }
    }

    private void Update()
    {
        // Returns a list of every spawn point Transform component
        // if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     Instantiate(bullet, transform.position, bullet.transform.rotation);
        // }
        
        _delay -= Time.deltaTime;
        if (_delay <= 0)
        {
            var x = Random.Range(0, _spawnLocations.Count);
            var randomPosition = _spawnLocations[x].position; // chooses random spawn location
            
            var shootDir = (_player.position - randomPosition).normalized;
            // Defines the direction of the bullet to the player
            
            var angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
            // Angle in degrees to rotate bullet on spawn
            
            _toRotation = Quaternion.Euler(0, 0, angle); // Coverts the angle to rotation

            Instantiate(bullet, randomPosition, _toRotation);
            _delay = 1 / fireRate;
        }
    }
}
