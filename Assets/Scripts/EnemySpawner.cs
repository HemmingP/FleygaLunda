using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Area")]
    [SerializeField] private float maxSpawnY = 5f;

    [Header("Destination")]
    [SerializeField] private float destinationX = -20f;
    [SerializeField] private float destinationMinY = 0f;
    [SerializeField] private float destinationMaxY = 5f;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Keep the spawner's X, randomly choose Y.
        Vector2 spawnPosition = new Vector2(
            transform.position.x,
            Random.Range(transform.position.y, maxSpawnY)
        );

        // Random destination Y.
        Vector2 destination = new Vector2(
            destinationX,
            Random.Range(destinationMinY, destinationMaxY)
        );

        // Work out direction from spawn -> destination.
        Vector2 direction = (destination - spawnPosition).normalized;

        // Spawn enemy.
        GameObject enemyObject = Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );

        // Give the enemy its movement information.
        Enemy enemy = enemyObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.SetDestination(destination, direction);
        }
    }
}
