using UnityEngine;

public class EnemyFactory  : MonoBehaviour
{
    [SerializeField] private float xMin = -13f;
    [SerializeField] private float xMax = 13f;
    [SerializeField] private float yMin = -6f;
    [SerializeField] private float yMax = 6f;
    
    [SerializeField] private float avoidanceRadius = 7f;
    public void CreateEnemyGroup(GameObject enemyPrefab, int count)
    {
        if (enemyPrefab == null || count <= 0)
        {
            Debug.LogError("Invalid parameters for CreateEnemyGroup.");
            return;
        }
        
        Vector3 groupPosition = GenerateRandomPosition();
        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
            Vector3 spawnPosition = groupPosition + offset;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    
    private Vector3 GenerateRandomPosition()
    {
        float randX, randY;
        do
        {
            randX = Random.Range(xMin, xMax);
            randY = Random.Range(yMin, yMax);
        } while (Vector2.Distance(new Vector2(randX, randY), Vector2.zero) < avoidanceRadius);

        return new Vector3(randX, randY, 0);
    }
}