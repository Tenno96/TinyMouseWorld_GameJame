using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRate = 2.0f;
    [SerializeField] private bool isStartSpawn = false;
    [SerializeField] private float lifeTime = 2.0f;

    void Start()
    {
        if (isStartSpawn)
        {
            StartSpawn();
        }
    }

    void OnEnable()
    {
        Bus<PlayerDeathEvent>.OnEvent += OnPlayerDeath;
    }

    void OnDisable()
    {
        StopSpawn();
        Bus<PlayerDeathEvent>.OnEvent -= OnPlayerDeath;
    }

    public void StartSpawn()
    {
        StartCoroutine("SpawnCoroutine");
    }

    public void StopSpawn()
    {
        StopCoroutine("SpawnCoroutine");
    }

    private void OnPlayerDeath(PlayerDeathEvent evt)
    {
        StopSpawn();
    }


    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            GameObject objectSpawned = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
            Destroy(objectSpawned, lifeTime);

            yield return new WaitForSeconds(spawnRate);
        }
    }
}