using UnityEngine;

public class RepeatingSpawner : MonoBehaviour
{
    [Header("スポーンするPrefab")]
    public GameObject prefab;

    [Header("スポーン間隔（秒）")]
    public float spawnInterval = 2f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(
            prefab,
            transform.position,
            Quaternion.identity
        );
    }
}
