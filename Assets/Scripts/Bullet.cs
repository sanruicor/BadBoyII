using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 30f;
    private float maxZPosition = 100f;


    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        if (transform.position.z >= maxZPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyObject") || other.CompareTag("EnemyObus"))
        {
            Destroy(gameObject);
        }
    }
}
