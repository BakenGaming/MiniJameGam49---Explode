using UnityEngine;

public class EnemyHandler : MonoBehaviour, IEnemyHandler
{
    #region Variables
    [SerializeField] private Transform firePoint;
    private EnemySO enemySO;
    private StatSystem _statSystem;
    private PlayerModifiers _modifiers;

    #endregion
    #region Initialize
    public void Initialize(EnemySO _e)
    {
        //enemySO = _e;
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
    #endregion
    #region Enemy Setup
    private void SetupEnemy()
    {
        _statSystem = new StatSystem(enemySO.enemyStatsSO);
        GetComponent<EnemyThinker>().ActivateBrain(this);
        GetComponent<EnemyMovementHandler>().InitializeMovement(this);
    }
    #endregion
}
