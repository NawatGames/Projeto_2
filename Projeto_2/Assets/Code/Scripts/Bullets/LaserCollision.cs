using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    public float damage;
    public float delay = 1f;

    private float _t;

    public void Start()
    {
        _t = 0;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        var colGameObject = col.gameObject;

        _t -= Time.deltaTime;

        if (colGameObject.CompareTag("Player"))
        {
            if (colGameObject.GetComponent<Shield>() == true)
            {
                if (!colGameObject.GetComponent<Shield>().isShielding && _t <= 0)
                {
                    colGameObject.GetComponent<Health>().RemoveHealth(damage);
                    Debug.Log(damage+" DAMAGE!");
                    _t = delay;
                }
            }
        }
        else if (colGameObject.CompareTag("Projectile"))
        {
            Destroy(colGameObject);
        }
    }
}