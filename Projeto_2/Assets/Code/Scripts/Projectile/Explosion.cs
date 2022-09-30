using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float bulletLifetime;
    
    [SerializeField] private GameObject fragmentObject;
    [SerializeField] private int numberOfFragments;
    
    private void Update()
    {
        bulletLifetime -= Time.deltaTime; // Controls the bullet lifeTime in seconds
        if (!(bulletLifetime <= 0)) return;
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        for (var i = 0; i < numberOfFragments; i++) // Repeats (the number of fragments) times
        {
            Instantiate(fragmentObject, transform.position, Quaternion.Euler(0, 0, 360 / numberOfFragments * i));
            // Creates a fragment on the same position as the gameObject and rotated by a fraction of 360 degrees
        }
    }
}