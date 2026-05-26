using UnityEngine;
using UnityEngine.Jobs;

public class RifleController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shotPoint;
    private float fireRate = 0.15f;
    private float nextFireTime = 0f;
    private AudioSource audioSource;
    [SerializeField] private AudioClip rifleshot;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shot()
    {
        if (Time.time >= nextFireTime)
        {
            SpawnBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void SpawnBullet()
    {
        Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);

        audioSource.PlayOneShot(rifleshot);
    }
}
