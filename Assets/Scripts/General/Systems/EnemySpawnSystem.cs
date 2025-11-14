using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    [SerializeField] private BoxCollider2D spawnZone;
    private LevelSO currentLevel;
    private bool readyToSpawn;
    private float spawnTimer;
    private Vector3 spawnPoint;
    private Collider2D objectCheck;

    public void InitializeSpawner(LevelSO _l)
    {
        currentLevel = _l;
        readyToSpawn = true;
    }

    void Update()
    {
        if(readyToSpawn) SpawnEnemy();
        
        UpdateTimers();
    }
    private void UpdateTimers()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0) readyToSpawn = true;
    }

    private void SpawnEnemy()
    {
        readyToSpawn = false;
        ChooseSpawnPoint();
        SpawnIndicatorPopup.Create(spawnPoint);
        spawnTimer = currentLevel.timeBetweenSpawnsStart;
    }

    private void  ChooseSpawnPoint()
    {
        spawnPoint = new Vector3(
            Random.Range(-spawnZone.bounds.extents.x, spawnZone.bounds.extents.x), 
            Random.Range(-spawnZone.bounds.extents.y, spawnZone.bounds.extents.y), 0);

        objectCheck = Physics2D.OverlapCircle(spawnPoint, 2f, StaticVariables.i.GetDeadzoneLayer());

        while (objectCheck != null)
        {
            spawnPoint = new Vector3(
                Random.Range(-spawnZone.bounds.extents.x, spawnZone.bounds.extents.x),
                Random.Range(-spawnZone.bounds.extents.y, spawnZone.bounds.extents.y), 0f);

            objectCheck = Physics2D.OverlapCircle(spawnPoint, 2f, StaticVariables.i.GetDeadzoneLayer());
        }
    }
}
