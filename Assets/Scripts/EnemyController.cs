using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected float speed = 6.0f;
    protected float minZPosition = -20.0f;


    protected virtual void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        if (transform.position.z <= minZPosition)
        {
            Destroy(gameObject);
        }
    }
}
