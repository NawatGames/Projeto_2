using UnityEngine;
using Vector3 = UnityEngine.Vector3;

// UNUSED
public class ScalingAnimation : MonoBehaviour
{
    public float lenght;
    public float thickness = 1;
    public float speed;

    private void FixedUpdate()
    {
        if (transform.localScale.x < lenght)
        {
            transform.localScale += Vector3.right * (Time.deltaTime * speed);
        }
    }
}