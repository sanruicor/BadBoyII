using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;
    private float minSpawnDelay = 1.6f;
    private float maxSpawnDelay = 3.5f;

    
    void Start()
    {
        StartCoroutine(SpawnSequence());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnSequence()
    {
        while (true)
        {
            SpawnEnemy();

            float nextDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(nextDelay);
        }
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = Random.value <= 0.1f ? tankPrefab : barrelPrefab;

        Transform selectedSpawnPoint = (Random.value > 0.5f) ? spawnPointLeft : spawnPointRight;

        if (prefabToSpawn != null && selectedSpawnPoint != null)
        {
            Instantiate(prefabToSpawn, selectedSpawnPoint.position, selectedSpawnPoint.rotation);
        }
    }
}
