using UnityEngine;
using System.Collections;

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
    }

    private void SpawnEnemy()
    {
        if (spawnTimer > 0 || !readyToSpawn) return;
        readyToSpawn = false;
        ChooseSpawnPoint();
        SpawnIndicatorPopup.Create(spawnPoint);
        StartCoroutine(IndicatorDelay());
    }

    IEnumerator IndicatorDelay()
    {
        yield return new WaitForSeconds(.6f);
        EnemyHandler newEnemy = ObjectPooler.DequeueObject<EnemyHandler>("Enemy");
        newEnemy.transform.position = spawnPoint;
        newEnemy.gameObject.SetActive(true);
        newEnemy.Initialize(currentLevel.levelEnemies[Random.Range(0, currentLevel.levelEnemies.Length)]);
        spawnTimer = currentLevel.timeBetweenSpawnsStart;
        readyToSpawn = true;
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
