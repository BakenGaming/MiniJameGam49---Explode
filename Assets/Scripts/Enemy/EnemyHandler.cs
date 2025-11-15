using System;
using UnityEngine;

public class EnemyHandler : MonoBehaviour, IEnemyHandler, IDamageable
{
    #region Variables
    public static event Action<EnemyHandler> OnEnemyRemoved;
    [SerializeField] private Transform firePoint;
    private EnemySO enemySO;
    private StatSystem _statSystem;
    private HealthSystem _healthSystem;
    private PlayerModifiers _modifiers;

    #endregion
    #region Initialize
    public void Initialize(EnemySO _e)
    {
        enemySO = _e;
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = _e.enemySprite;
        SetupEnemy();
    }

    #endregion

    #region Get Functions
    public StatSystem GetStatSystem()
    {
        return _statSystem;
    }
    #endregion
    #region Handle Enemy Functions
    public void TakeDamage(int _damage)
    {
        _healthSystem.LoseHealth(_damage);
        if(_healthSystem.GetCurrentHealth() == 0)
        {
            CollectableObjectHandler newCollectable = ObjectPooler.DequeueObject<CollectableObjectHandler>("Collectable");
            newCollectable.transform.position = transform.position;
            newCollectable.transform.rotation = transform.rotation;
            newCollectable.gameObject.SetActive(true);
            newCollectable.InitializeCollectable(enemySO.lootBag[0]);
            OnEnemyRemoved?.Invoke(this);
            ObjectPooler.EnqueueObject(this, "Enemy");
        }
    }

    private bool GetBonusDrop()
    {
        if (UnityEngine.Random.Range(0f,100f) >= 75f) return true;
        else return false;
    }
    #endregion
    #region Enemy Setup
    private void SetupEnemy()
    {
        _statSystem = new StatSystem(enemySO.enemyStatsSO);
        _healthSystem = new HealthSystem(_statSystem.GetHealth());
        GetComponent<EnemyThinker>().ActivateBrain(this);
        GetComponent<EnemyMovementHandler>().InitializeMovement(this);
    }
    #endregion
    #region Handle Collision
    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlanetHandler _handler = other.GetComponent<PlanetHandler>();
        if(_handler != null)
        {
            DamagePopup.Create(GameManager.i.GetPlanetCenter().position,_statSystem.damage, false);
            _handler.TakeDamage(_statSystem.damage);
            OnEnemyRemoved?.Invoke(this);
            ObjectPooler.EnqueueObject(this, "Enemy");
        }
    }
    #endregion
}
