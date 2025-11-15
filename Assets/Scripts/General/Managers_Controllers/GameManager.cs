using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    private static GameManager _i;
    public static GameManager i { get { return _i; } }
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private WeaponSO startingWeapon;
    [SerializeField] private EnemySpawnSystem _spawner;
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private Transform projectilePool, enemyPool, objectPool;
    [SerializeField] private UIController _uiController;
    private int levelIndex = 0;
    private GameObject playerGO;
    private GameObject planet;
    private bool isPaused;


    #endregion
    
    #region Initialize
    private void Awake() 
    {
        _i = this;  
        SetupObjectPools();  
        Initialize();
    }

    private void Initialize() 
    {
        SpawnPlanetObject();
    }

    private void SpawnPlanetObject()
    {
        planet = Instantiate(GameAssets.i.pfPlanetObject, spawnPoint);
        planet.transform.parent = null;
        planet.GetComponent<PlanetHandler>().Initialize();
        SpawnPlayerObject();
    }
    private void SpawnPlayerObject()
    {
        playerGO = Instantiate(GameAssets.i.pfPlayerObject, planet.transform.Find("ShipSpawnPoint"));
        playerGO.transform.parent = planet.transform;
        playerGO.GetComponent<IHandler>().Initialize(startingWeapon);
        InitializeUI();        
    }
    private void InitializeUI()
    {
        _uiController.Initialize();
        SetupSpawner();
    }

    private void SetupSpawner()
    {
        _spawner.InitializeSpawner(levels[levelIndex]);
    }
    public void StartNewLevel()
    {
        Debug.Log("Start New Level");
    }

    public void SetupObjectPools()
    {
        ObjectPooler.SetupPool(GameAssets.i.pfProjectile.GetComponent<Projectile>(), 20, "Player Projectile");
        ObjectPooler.SetupPool(GameAssets.i.pfProjectile.GetComponent<Projectile>(), 20, "Enemy Projectile");
        ObjectPooler.SetupPool(GameAssets.i.pfEnemyObject.GetComponent<EnemyHandler>(), 20, "Enemy");
        ObjectPooler.SetupPool(GameAssets.i.pfCollectable.GetComponent<CollectableObjectHandler>(), 20, "Collectable");
    }
    #endregion
    #region Game Manager Functions
    public void PauseGame(){if(isPaused) return; else isPaused = true;}
    public void UnPauseGame(){if(isPaused) isPaused = false; else return;}
    public Transform GetPoolLocation(string _key)
    {
        if(_key == "Enemy") return enemyPool; 
        else if(_key == "Player Projectile" || _key == "Enemy Projectile") return projectilePool;
        else return objectPool;
    }
    public GameObject GetPlayerGO() { return playerGO; }
    public Transform GetPlanetCenter(){return planet.transform;}
    public bool GetIsPaused() { return isPaused; }
    #endregion
}
