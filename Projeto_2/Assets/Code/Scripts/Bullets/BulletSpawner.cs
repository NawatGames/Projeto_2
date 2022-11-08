using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletSpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn = new List<GameObject>();
    public float fireRate;
    
    private float _delay;
    private Transform _player;
    private Quaternion _toRotation;
    private readonly List<Transform> _spawnLocations = new List<Transform>();

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        // Gets the player Transform component

        foreach (Transform tr in transform)
        {
            _spawnLocations.Add(tr);
        }
        
        _delay = 1 / fireRate; 
    }

    private void Update()
    {
        _delay -= Time.deltaTime;
        if (_delay <= 0)
        {
            var i = Random.Range(0, _spawnLocations.Count);
            var randomSpawn = _spawnLocations[i]; // Chooses random spawn location
            var spawnPosition = randomSpawn.position;

            var t = Random.Range(0, objectsToSpawn.Count);
            var randomBullet = objectsToSpawn[t]; // Chooses random bullet from prefabs list
            
            var shootDir = (_player.position - spawnPosition).normalized; 
            var angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
            _toRotation = Quaternion.Euler(0, 0, angle); // Angle to rotate bullet

            Instantiate(randomBullet, spawnPosition, _toRotation);

            _delay = 1 / fireRate; // Converts fire rate to bullets per second
        }
    }
}