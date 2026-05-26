using Unity.VisualScripting;
using UnityEngine;

public class Barrel : EnemyController
{
    private int health = 280;
    private int damage = 20;
    private AudioSource audioSource;
    [SerializeField] private AudioClip barrelDestruction;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            health -= damage;

            if (health <= 0)
            {
                Destroy(gameObject);
                audioSource.PlayOneShot(barrelDestruction);
            }
        }
    }
}
