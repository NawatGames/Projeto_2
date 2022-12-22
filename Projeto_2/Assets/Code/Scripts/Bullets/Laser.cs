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
        // foreach (GameObject obj in particles.transform)
        // {
        //     var particleSystemShape = obj.GetComponent<ParticleSystem>().shape;
        //     particleSystemShape.scale += Vector3.right * laserLenght;
        // }
        StartCoroutine(FireLaser());
    }

    private IEnumerator FireLaser()
    {
        yield return new WaitForSeconds(warningDuration);
        laserWarning.SetActive(false);
        laserFire.SetActive(true);

        yield return new WaitForSeconds(laserLifetime);
        Destroy(gameObject);
    }
}