using UnityEngine;

public class EnemyFactory  : MonoBehaviour
{
    public void CreateEnemyGroup(GameObject enemyPrefab, int count)
    {
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
            randX = Random.Range(-13f, 13f);
            randY = Random.Range(-6f, 6f);
        } while (Vector2.Distance(new Vector2(randX, randY), Vector2.zero) < 7f);

        return new Vector3(randX, randY, 0);
    }
}