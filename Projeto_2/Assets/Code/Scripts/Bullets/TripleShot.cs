using System;
using System.Collections;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float delay = 0.5f;
    public float amountOfBullets;

    private void OnEnable()
    {
        StartCoroutine(TripleShotAttack(objectToSpawn, delay, amountOfBullets));
    }
    
    private IEnumerator TripleShotAttack(GameObject bullet, float f, float amountOfBullets)
    {   
        for (var i = 0; i < amountOfBullets; i++)
        {
            var transform1 = transform;
            Instantiate(bullet, transform1.position, transform1.rotation);
            yield return new WaitForSeconds(f);
        }

        Destroy(gameObject);
    }
}