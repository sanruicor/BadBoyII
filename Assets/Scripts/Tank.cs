using UnityEngine;

public class Tank : EnemyController
{
    [SerializeField] private GameObject obusPrefab;
    [SerializeField] private Transform shotPoint;
    private float detectionDistance = 50.0f;
    private float fireRate = 3.0f;
    private float nextFireTime = 0f;


    protected override void Update()
    {
        base.Update();

        DetectAndAttack();
    }

    private void DetectAndAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out hit, detectionDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (Time.time >= nextFireTime)
                {
                    FireShell();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    private void FireShell()
    {
        Instantiate(obusPrefab, shotPoint.position, shotPoint.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        if (shotPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(shotPoint.position, shotPoint.forward * detectionDistance);
        }
    }
}
