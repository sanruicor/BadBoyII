using UnityEngine;

public class Obus : MonoBehaviour
{
    private float speed = 20.0f;
    private float minZPosition = -20.0f;
    

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (transform.position.z <= minZPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}
