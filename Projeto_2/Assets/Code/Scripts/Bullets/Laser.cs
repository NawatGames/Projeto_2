using System;
using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laserWarning;
    public GameObject laserFire;

    public float laserLenght;
    public float warningDuration;
    public float laserLifetime;
    public GameObject particles;

    private void OnEnable()
    {
        transform.localScale += Vector3.right * laserLenght;
        StartCoroutine(FireLaser());
        StartCoroutine(Blink());
    }

    private IEnumerator FireLaser()
    {
        yield return new WaitForSeconds(warningDuration);
        laserWarning.SetActive(false);
        laserFire.SetActive(true);

        yield return new WaitForSeconds(laserLifetime);
        Destroy(gameObject);
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            laserWarning.GetComponent<SpriteRenderer>().enabled = !laserWarning.GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(0.2f);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}