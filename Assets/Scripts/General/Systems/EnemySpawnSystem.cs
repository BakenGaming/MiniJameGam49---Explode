using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemySpawnSystem : MonoBehaviour
{
    public static event Action<float> OnTimerChange;
    public static event Action OnLevelEnd;
    [SerializeField] private BoxCollider2D spawnZone;
    private LevelSO currentLevel;
    private List<EnemyHandler> activeEnemies;
    private List<CollectableObjectHandler> activeCollectables;
    private bool readyToSpawn;
    private float spawnTimer, gameTimer;
    private Vector3 spawnPoint;
    private Collider2D objectCheck;

    public void InitializeSpawner(LevelSO _l)
    {
        activeEnemies = new List<EnemyHandler>();
        activeCollectables = new List<CollectableObjectHandler>();
        currentLevel = _l;
        gameTimer = _l.levelLengthInSeconds;
        OnTimerChange?.Invoke(gameTimer);
        EnemyHandler.OnEnemyRemoved += RemoveEnemy;  
        CollectableObjectHandler.OnCollectableCreated += AddToCollectable;      
        CollectableObjectHandler.OnCollectableRemoved += RemoveFromCollectables;      
        readyToSpawn = true;
    }

    void Update()
    {
        if (GameManager.i.GetIsPaused()) return;

        if(readyToSpawn) SpawnEnemy();

        if(!readyToSpawn && gameTimer == 0 && activeEnemies.Count == 0 && activeCollectables.Count == 0)
        {
            OnLevelEnd?.Invoke();
        }
        
        UpdateTimers();
    }
    private void UpdateTimers()
    {
        spawnTimer -= Time.deltaTime;
        gameTimer -= Time.deltaTime;
        if(gameTimer < 0)
        {   readyToSpawn = false;
            gameTimer = 0f;
            OnTimerChange?.Invoke(gameTimer);
        }
        else OnTimerChange?.Invoke(gameTimer);
    }
    IEnumerator EndDelay()
    {
        yield return new WaitForSecondsRealtime(2f);
        
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
        newEnemy.Initialize(currentLevel.levelEnemies[UnityEngine.Random.Range(0, currentLevel.levelEnemies.Length)]);
        spawnTimer = currentLevel.timeBetweenSpawnsStart;
        activeEnemies.Add(newEnemy);
        readyToSpawn = true;
    }
    private void  ChooseSpawnPoint()
    {
        spawnPoint = new Vector3(
            UnityEngine.Random.Range(-spawnZone.bounds.extents.x, spawnZone.bounds.extents.x), 
            UnityEngine.Random.Range(-spawnZone.bounds.extents.y, spawnZone.bounds.extents.y), 0);

        objectCheck = Physics2D.OverlapCircle(spawnPoint, 2f, StaticVariables.i.GetDeadzoneLayer());

        while (objectCheck != null)
        {
            spawnPoint = new Vector3(
                UnityEngine.Random.Range(-spawnZone.bounds.extents.x, spawnZone.bounds.extents.x),
                UnityEngine.Random.Range(-spawnZone.bounds.extents.y, spawnZone.bounds.extents.y), 0f);

            objectCheck = Physics2D.OverlapCircle(spawnPoint, 2f, StaticVariables.i.GetDeadzoneLayer());
        }
    }
    private void RemoveEnemy(EnemyHandler _e)
    {
        activeEnemies.Remove(_e);
    }
    private void AddToCollectable(CollectableObjectHandler _c)
    {
        activeCollectables.Add(_c);
    }
    private void RemoveFromCollectables(CollectableObjectHandler _c)
    {
        activeCollectables.Remove(_c);
    }
}
